using CashPilot.Domain.DTOs.Incomes.Request;
using CashPilot.Domain.DTOs.Incomes.Response;
using CashPilot.Domain.DTOs.Logins.Response;

namespace CashPilot.Application.Interfaces.Services;

public interface ILoginService
{
    Task<ResponseCreateLoginDto> LogUserAsync(string email, string password);
    Task ForgotPasswordAsync(string email);
}