using MediatR;
using Application.Common.Models;
using Application.Common.Reponses;

namespace Application.Request;

public class GetUserProfileR : IRequest<SingleResponse>
{
    public Guid UserId { get; set; }
}


