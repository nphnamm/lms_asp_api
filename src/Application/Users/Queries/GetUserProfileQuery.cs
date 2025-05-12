using MediatR;
using Application.Common.Models;

public class GetUserProfileQuery : IRequest<UserProfileDto>
{
    public Guid UserId { get; set; }
}