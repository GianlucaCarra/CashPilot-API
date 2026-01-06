namespace CashPilot.Application.Interfaces.Services.Caching;

public interface IResetPasswordAttemptService
{
    Task<int> IncrementAttemptAsync(string email);
    Task ResetAttemptsAsync(string email);
}