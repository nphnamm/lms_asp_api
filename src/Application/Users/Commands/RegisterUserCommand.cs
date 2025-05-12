using MediatR;
using Application.Common.Models;

namespace Application.Users.Commands;

public class RegisterUserCommand : IRequest<Result<UserResponse>>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}