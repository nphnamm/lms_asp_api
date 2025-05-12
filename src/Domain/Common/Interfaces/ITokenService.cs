using Domain.Entities;

namespace Domain.Common.Interfaces;

public interface ITokenService
{
    Task<string> GenerateAccessToken(User user);
    string GenerateRefreshToken();
} 