using MediatR;
using Application.Common.Models;
using Microsoft.AspNetCore.Identity;
using Domain.Entities;
using Application.Request;
using Application.Common.Reponses;

namespace Application.Users.Queries;

public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileR, SingleResponse>
{
    private readonly UserManager<User> _userManager;

    public GetUserProfileQueryHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<SingleResponse> Handle(GetUserProfileR request, CancellationToken cancellationToken)
    {
        var response = new SingleResponse();
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        var userProfile = User.CreateUserDto(user.Id, user.Email, user.PhoneNumber, user.UserName);

        if (user == null)
            return response.SetError("User not found");
        

        return response.SetSuccess(userProfile);

 
    }
}