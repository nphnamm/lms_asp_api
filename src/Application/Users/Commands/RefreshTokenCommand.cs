using MediatR;
using Application.Common.Models;

namespace Application.Users.Commands;

public class RefreshTokenCommand : IRequest<AuthenticationResponse>
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}