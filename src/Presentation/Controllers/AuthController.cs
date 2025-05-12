using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Application.Common.Models;
using Application.Users.Commands;
using Application.Common.Exceptions;

// Presentation/Controllers/AuthController.cs
[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("login")]
    public async Task<ActionResult<GeneralServiceResponseDto<AuthenticationResponse>>> Login(AuthenticateUserCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return Ok(GeneralServiceResponseDto<AuthenticationResponse>.Success(result, "Login successful"));
        }
        catch (UnauthorizedException ex)
        {
            return Unauthorized(GeneralServiceResponseDto<AuthenticationResponse>.Failure(
                "Authentication failed",
                401,
                new List<string> { ex.Message }
            ));
        }
    }
    
    [HttpPost("register")]
    public async Task<ActionResult<GeneralServiceResponseDto<UserResponse>>> Register(RegisterUserCommand command)
    {
        var result = await _mediator.Send(command);
        
        if (!result.IsSuccess)
        {
            return BadRequest(GeneralServiceResponseDto<UserResponse>.Failure(
                "Registration failed",
                400,
                new List<string> { result.Error }
            ));
        }
        
        return Ok(GeneralServiceResponseDto<UserResponse>.Success(result.Data, "Registration successful"));
    }
    
    [HttpPost("refresh-token")]
    public async Task<ActionResult<GeneralServiceResponseDto<AuthenticationResponse>>> RefreshToken(RefreshTokenCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return Ok(GeneralServiceResponseDto<AuthenticationResponse>.Success(result, "Token refreshed successfully"));
        }
        catch (UnauthorizedException ex)
        {
            return Unauthorized(GeneralServiceResponseDto<AuthenticationResponse>.Failure(
                "Token refresh failed",
                401,
                new List<string> { ex.Message }
            ));
        }
    }
}

