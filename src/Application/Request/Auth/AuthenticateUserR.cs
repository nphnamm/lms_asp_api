using MediatR;
using Application.Common.Models;
using Application.Common.Reponses;
namespace Application.Request;

public class AuthenticateUserR : IRequest<SingleResponse>
{
    public string Email { get; set; }
    public string Password { get; set; }
}
