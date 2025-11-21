using System.Net;
using System.Net.Mail;
using CashPilot.Application.Configuration;
using SendGrid;
using SendGrid.Helpers.Errors.Model;
using SendGrid.Helpers.Mail;

namespace CashPilot.Application.Services;

public class EmailService
{
    private readonly EmailSettings _settings;

    public EmailService(EmailSettings settings, string apiKey)
    {
        _settings = settings;
    }

    public async Task SendVerificationEmail(string email, string token)
    {
        var validateString = "a";
    }
    
    private async Task SendToClientAsync(string email, string subject, string body)
    {
        var client = new SmtpClient
        {
            EnableSsl = true,
            Credentials = new NetworkCredential(_settings.FromEmail, _settings.Password)
        };
        
        var message = new MailMessage
        {
            From = new MailAddress(_settings.FromEmail, _settings.FromName),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };
        
        message.To.Add(new MailAddress(email));
        
        await client.SendMailAsync(message);
    }
}