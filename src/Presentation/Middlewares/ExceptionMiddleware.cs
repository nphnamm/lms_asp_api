// Presentation/Middlewares/ExceptionMiddleware.cs
using Microsoft.AspNetCore.Http;
using System.Net;

namespace Presentation.Middlewares;

public class UnauthorizedException : Exception
{
    public UnauthorizedException(string message) : base(message)
    {
    }
}

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    
    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (UnauthorizedException ex)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsJsonAsync(new
            {
                StatusCode = 401,
                Message = ex.Message
            });
        }
        // Các exception khác...
    }
}