using MediatR;
using Application.Common.Models;
using Application.Common.Reponses;

namespace Application.Request;

public class RegisterUserR : IRequest<SingleResponse>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
