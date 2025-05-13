using MediatR;
using Microsoft.AspNetCore.Identity;
using Domain.Entities;
using Application.Common.Models;
using Application.Common.Reponses;
using static Application.Constants.Error;
using Application.Request;

namespace Application.Users.Commands;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserR, SingleResponse>
{
    private readonly UserManager<User> _userManager;

    public RegisterUserCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<SingleResponse> Handle(RegisterUserR request, CancellationToken cancellationToken)
    {

        var res = new SingleResponse();

        // Kiểm tra email đã tồn tại
        var existingUser = await _userManager.FindByEmailAsync(request.Email);
        if (existingUser != null)
            return res.SetError(nameof(E001), E001);
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
            return res.SetError(nameof(E001), E001);

        // Assign Instructor role
        await _userManager.AddToRoleAsync(user, "Instructor");

        // Trả về thông tin user đã tạo
        return res.SetSuccess(user);
    }
}