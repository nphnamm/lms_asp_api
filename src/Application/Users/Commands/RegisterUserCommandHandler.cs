using MediatR;
using Microsoft.AspNetCore.Identity;
using Domain.Entities;
using Application.Common.Models;

namespace Application.Users.Commands;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<UserResponse>>
{
    private readonly UserManager<User> _userManager;
    
    public RegisterUserCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    
    public async Task<Result<UserResponse>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        // Kiểm tra email đã tồn tại
        var existingUser = await _userManager.FindByEmailAsync(request.Email);
        if (existingUser != null)
            return Result<UserResponse>.Failure("Email đã được đăng ký");
        
        // Tạo user mới
        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            UserName = request.Email,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };
        
        var result = await _userManager.CreateAsync(user, request.Password);
        
        if (!result.Succeeded)
            return Result<UserResponse>.Failure(string.Join(", ", result.Errors.Select(e => e.Description)));
        
        // Trả về thông tin user đã tạo
        return Result<UserResponse>.Success(new UserResponse
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            IsActive = user.IsActive,
            CreatedAt = user.CreatedAt
        });
    }
}