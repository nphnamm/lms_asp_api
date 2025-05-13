using Application.Common.Exceptions;
using Application.Common.Models;
using Application.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> Login([FromBody] AuthenticateUserR command)
    {
    
        var result = await _mediator.Send(command);
        return Ok(result);


    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserR command)
    {
        var result = await _mediator.Send(command);

        if (!result.Succeeded)
            return BadRequest(result.Message);

        return Ok(result);
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken(RefreshTokenR command)
    {

        var result = await _mediator.Send(command);
        return Ok(result);

    }
}

