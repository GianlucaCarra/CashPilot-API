using CashPilot.Application.Configuration;
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace CashPilot.Application.Services;

public class EmailService
{
    private readonly EmailSettings _settings;
    private readonly EmailTemplateService _templateService;

    public EmailService(EmailSettings settings, EmailTemplateService templateService)
    {
        _settings = settings;
        _templateService = templateService;
    }

    public async Task SendVerificationEmail(string name, string email, string token)
    {
        var verificationUrl = $"{_settings.BaseUrl}/api/verification/validate?token={token}";

        var body = await _templateService.GetVerificationEmailAsync(name, verificationUrl);

        await SendToClientAsync(name, email, $"Hi {name}, activate your account to start using!", body);
    }
    
    private async Task SendToClientAsync(string name, string email, string subject, string body)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(_settings.FromName, _settings.FromEmail));
        message.To.Add(new MailboxAddress(name,email));
        message.Subject = subject;
        message.Body = new TextPart("html")
        {
            Text = body
        };

        using var client = new SmtpClient();
        await client.ConnectAsync(_settings.SmtpServer, _settings.SmtpPort, true);
        
        await client.AuthenticateAsync(_settings.FromEmail, _settings.Password);

        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }
}