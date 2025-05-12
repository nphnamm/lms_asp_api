using System.Security.Claims;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Presentation.Middleware;

public class UserContextMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<UserContextMiddleware> _logger;

    public UserContextMiddleware(RequestDelegate next, ILogger<UserContextMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, UserManager<User> userManager)
    {
        try
        {
            if (context.User.Identity?.IsAuthenticated == true)
            {
                var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!string.IsNullOrEmpty(userId) && Guid.TryParse(userId, out var userGuid))
                {
                    var user = await userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        // Add user to HttpContext.Items for use in controllers
                        context.Items["CurrentUser"] = user;
                        
                        // Log user information
                        _logger.LogInformation(
                            "User authenticated: {UserId}, {Email}, Roles: {Roles}",
                            user.Id,
                            user.Email,
                            string.Join(", ", await userManager.GetRolesAsync(user))
                        );
                    }
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing user context");
        }

        await _next(context);
    }
}

// Extension method to make it easier to use the middleware
public static class UserContextMiddlewareExtensions
{
    public static IApplicationBuilder UseUserContext(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<UserContextMiddleware>();
    }
} 