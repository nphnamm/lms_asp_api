using MediatR;
using Application.Common.Reponses;
using Application.Request;
using Infrastructure.Services;
using static Application.Constants.Error;
using Microsoft.AspNetCore.Identity;
using Domain.Entities;

namespace Application.Users.Commands;

public class VerifyOtpCommandHandler : IRequestHandler<VerifyOtpR, SingleResponse>
{
    private readonly IOtpService _otpService;
    private readonly UserManager<User> _userManager;

    public VerifyOtpCommandHandler(IOtpService otpService, UserManager<User> userManager)
    {
        _otpService = otpService;
        _userManager = userManager;
    }

    public async Task<SingleResponse> Handle(VerifyOtpR request, CancellationToken cancellationToken) 
    {
        var res = new SingleResponse();

        var isValid = await _otpService.VerifyOtpAsync(request.Token, request.Otp);
        if (!isValid)
            return res.SetError(nameof(E102), E102);

        // Get user from token
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
            return res.SetError(nameof(E119), E119);

        // Activate user account
        user.IsActive = true;
        user.EmailConfirmed = true;
        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
            return res.SetError(nameof(E004), E004);

        return res.SetSuccess(new { Message = "OTP verified successfully. You can now login." });
    }
} 