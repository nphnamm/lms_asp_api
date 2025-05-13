using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services;

public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string body);
}

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    private readonly string _smtpServer;
    private readonly int _smtpPort;
    private readonly string _smtpUsername;
    private readonly string _smtpPassword;
    private readonly string _fromEmail;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
        _smtpServer = _configuration["Email:SmtpServer"] ?? throw new InvalidOperationException("SMTP server not configured");
        _smtpPort = int.Parse(_configuration["Email:SmtpPort"] ?? "587");
        _smtpUsername = _configuration["Email:SmtpUsername"] ?? throw new InvalidOperationException("SMTP username not configured");
        _smtpPassword = _configuration["Email:SmtpPassword"] ?? throw new InvalidOperationException("SMTP password not configured");
        _fromEmail = _configuration["Email:FromEmail"] ?? throw new InvalidOperationException("From email not configured");
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        using var client = new SmtpClient(_smtpServer, _smtpPort)
        {
            EnableSsl = true,
            Credentials = new System.Net.NetworkCredential(_smtpUsername, _smtpPassword)
        };

        using var message = new MailMessage
        {
            From = new MailAddress(_fromEmail),
            Subject = subject,
            Body = body,
            IsBodyHtml = false
        };
        message.To.Add(to);

        await client.SendMailAsync(message);
    }
} 
 