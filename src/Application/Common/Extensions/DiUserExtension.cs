using Application.Users.Commands;
using Application.Common.Reponses;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Application.Request;
using Application.Request;
namespace Application.Common.Extensions;


/// <summary>
/// DI extension
/// </summary>
public static class DiUserExtension
{
    #region -- Methods --

    /// <summary>
    /// Add DI for include commands and queries
    /// </summary>
    /// <param name="p">MediatRServiceConfiguration</param>
    /// <param name="life">ServiceLifetime</param>
    public static void AddDiUser(this MediatRServiceConfiguration p, ServiceLifetime life = ServiceLifetime.Scoped)
    {
        p.AddUserCommands(life);
        p.AddUserQueries(life);
    }

    /// <summary>
    /// Add commands handler
    /// </summary>
    /// <param name="p">MediatRServiceConfiguration</param>
    /// <param name="life">ServiceLifetime</param>
    public static void AddUserCommands(this MediatRServiceConfiguration p, ServiceLifetime life = ServiceLifetime.Scoped)
    {
        p.AddBehavior<IRequestHandler<RegisterUserR, SingleResponse>, RegisterUserCommandHandler>(life);
        p.AddBehavior<IRequestHandler<AuthenticateUserR, SingleResponse>, AuthenticateUserCommandHandler>(life);
        // p.AddBehavior<IRequestHandler<RefreshTokenR, SingleResponse>, RefreshTokenCommandHandler>(life);
    }

    /// <summary>
    /// Add queries handler
    /// </summary>
    /// <param name="p">MediatRServiceConfiguration</param>
    /// <param name="life">ServiceLifetime</param>
    public static void AddUserQueries(this MediatRServiceConfiguration p, ServiceLifetime life = ServiceLifetime.Scoped)
    {
        // p.AddBehavior<IRequestHandler<GetUserProfileR, SingleResponse>, GetUserProfileQueryHandler>(life);
    }

    #endregion
}
