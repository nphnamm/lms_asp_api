using MediatR;
using Application.Common.Models;
using Application.Common.Reponses;

namespace Application.Request;

public class VerifyOtpR : IRequest<SingleResponse>
{
    public string Otp { get; set; }
    public string Token { get; set; }
    public string Email { get; set; }
} 