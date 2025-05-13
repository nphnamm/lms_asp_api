using MediatR;
using Application.Common.Models;
using Application.Common.Reponses;
namespace Application.Request;

public class RefreshTokenR : IRequest<SingleResponse>
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}