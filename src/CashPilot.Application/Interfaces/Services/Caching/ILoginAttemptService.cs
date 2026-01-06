namespace CashPilot.Application.Interfaces.Services.Caching;

public interface ILoginAttemptService
{
    Task<int> IncrementAttemptAsync(string email);
    Task ResetAttemptsAsync(string email);
}