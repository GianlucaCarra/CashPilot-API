namespace CashPilot.Application.Interfaces.Services;

public interface IEmailService
{
    Task SendVerificationEmailAsync(string name, string email, string token);
    Task SendWelcomeEmailAsync(string name, string email);
    Task SendResetPasswordEmailAsync(string name, string email, string token);
}