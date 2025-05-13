using System.Security.Cryptography;
using Microsoft.Extensions.Caching.Memory;
using Domain.Entities;
using Domain.Common.Interfaces;

namespace Infrastructure.Services;

public class OtpResponse
{
    public string Token { get; set; }
    public string Otp { get; set; }
}

public interface IOtpService
{
    Task<OtpResponse> GenerateOtpAsync(User user);
    Task<bool> VerifyOtpAsync(string token, string otp);
}

public class OtpService : IOtpService
{
    private readonly IMemoryCache _cache;
    private readonly IEmailService _emailService;
    private readonly ITokenService _tokenService;
    private const int OTP_LENGTH = 6;
    private const int OTP_EXPIRY_MINUTES = 5;

    public OtpService(IMemoryCache cache, IEmailService emailService, ITokenService tokenService)
    {
        _cache = cache;
        _emailService = emailService;
        _tokenService = tokenService;
    }

    public async Task<OtpResponse> GenerateOtpAsync(User user)
    {
        var otp = GenerateOtp();
        var token = await _tokenService.GenerateOtpToken(user);
        var cacheKey = $"otp_{token}";
        _cache.Set(cacheKey, otp, TimeSpan.FromMinutes(OTP_EXPIRY_MINUTES));

        await _emailService.SendEmailAsync(
            user.Email,
            "Verify your email",
            $"Your verification code is: {otp}. This code will expire in {OTP_EXPIRY_MINUTES} minutes."
        );

        return new OtpResponse { Token = token, Otp = otp };
    }

    public async Task<bool> VerifyOtpAsync(string token, string otp)
    {
        var cacheKey = $"otp_{token}";
        if (!_cache.TryGetValue(cacheKey, out string storedOtp))
        {
            return false;
        }

        if (storedOtp != otp)
        {
            return false;
        }

        // Remove the OTP from cache after successful verification
        _cache.Remove(cacheKey);
        return true;
    }

    private string GenerateOtp()
    {
        var random = new byte[OTP_LENGTH];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(random);

        var otp = new string(random.Select(b => (char)('0' + (b % 10))).ToArray());
        return otp;
    }
}