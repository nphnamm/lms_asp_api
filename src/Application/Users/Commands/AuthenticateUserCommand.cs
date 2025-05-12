using MediatR;
using Microsoft.AspNetCore.Identity;
using Domain.Entities;
using Application.Common.Models;
using Domain.Common.Interfaces;
using Application.Common.Exceptions;

namespace Application.Users.Commands;

public class AuthenticateUserCommand : IRequest<AuthenticationResponse>
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, AuthenticationResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;
    
    public AuthenticateUserCommandHandler(UserManager<User> userManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }
    
    public async Task<AuthenticationResponse> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
    {
        // Bước 1: Kiểm tra user tồn tại
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null) throw new UnauthorizedException("Invalid credentials");
        
        // Bước 2: Verify password
        var isValidPassword = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!isValidPassword) throw new UnauthorizedException("Invalid credentials");
        
        // Bước 3: Tạo tokens
        var accessToken = await _tokenService.GenerateAccessToken(user);
        var refreshToken = _tokenService.GenerateRefreshToken();
        
        // Bước 4: Lưu refresh token vào user
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7); // Ví dụ: 7 ngày
        await _userManager.UpdateAsync(user);
        
        // Bước 5: Trả về response
        return new AuthenticationResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresIn = 3600 // 1 giờ
        };
    }
}