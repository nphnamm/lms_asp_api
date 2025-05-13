using MediatR;
using Microsoft.AspNetCore.Identity;
using Domain.Entities;
using Application.Common.Models;
using Domain.Common.Interfaces;
using Application.Common.Exceptions;
using Application.Request;
using Application.Common.Reponses;
using static Application.Constants.Error;
namespace Application.Users.Commands;


public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserR, SingleResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;
    
    public AuthenticateUserCommandHandler(UserManager<User> userManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }
    
    public async Task<SingleResponse> Handle(AuthenticateUserR request, CancellationToken cancellationToken)
    {
        var res = new SingleResponse();
        // Bước 1: Kiểm tra user tồn tại
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null) return res.SetError(nameof(E001), E001, "User not found");
        
        // Bước 2: Verify password
        var isValidPassword = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!isValidPassword) return res.SetError(nameof(E001), E001, "Invalid credentials");
        
        // Bước 3: Tạo tokens
        var accessToken = await _tokenService.GenerateAccessToken(user);
        var refreshToken = _tokenService.GenerateRefreshToken();
        
        // Bước 4: Lưu refresh token vào user
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7); // Ví dụ: 7 ngày
        await _userManager.UpdateAsync(user);
        
        // Bước 5: Trả về response
        return res.SetSuccess(new AuthenticationResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresIn = 3600 // 1 giờ
        });
    }
}