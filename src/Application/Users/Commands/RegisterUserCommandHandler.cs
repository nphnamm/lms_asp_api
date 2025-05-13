using MediatR;
using Microsoft.AspNetCore.Identity;
using Domain.Entities;
using Application.Common.Models;
using Application.Common.Reponses;
using static Application.Constants.Error;
using Application.Request;
using Infrastructure.Services;
using Infrastructure.Identity;

namespace Application.Users.Commands;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserR, SingleResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly IOtpService _otpService;

    public RegisterUserCommandHandler(UserManager<User> userManager, IOtpService otpService)
    {
        _userManager = userManager;
        _otpService = otpService;
    }

    public async Task<SingleResponse> Handle(RegisterUserR request, CancellationToken cancellationToken)
    {
        var res = new SingleResponse();

        // Check if email exists
        var existingUser = await _userManager.FindByEmailAsync(request.Email);
        if (existingUser != null)
            return res.SetError(
                "500", "Email already exists");

        var tempUser = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            UserName = request.Email,
            CreatedAt = DateTime.UtcNow,
            IsActive = false // User is not active until OTP is verified
        };

        // Generate and send OTP
        var otpResponse = await _otpService.GenerateOtpAsync(tempUser);

        // Store the user with a temporary password
        var result = await _userManager.CreateAsync(tempUser, request.Password);
        if (!result.Succeeded)
            return res.SetError(nameof(E001), E001);

        return res.SetSuccess(new { Token = otpResponse.Token, Message = "OTP sent to your email. Please verify to complete registration." });
    }
}
