namespace CashPilot.Application.Interfaces.Services;

public interface IEmailTemplateService
{
    Task<string> GetVerificationEmailAsync(string name, string verificationUrl);
    Task<string> GetResetPasswordEmailAsync(string verificationUrl);
    Task<string> GetWelcomeEmailAsync(string name);
}