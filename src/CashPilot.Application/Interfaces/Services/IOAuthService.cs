using CashPilot.Domain.DTOs.Incomes.Request;
using CashPilot.Domain.DTOs.Incomes.Response;
using CashPilot.Domain.DTOs.Logins.Response;
using CashPilot.Domain.DTOs.OAuth.Request;

namespace CashPilot.Application.Interfaces.Services;

public interface IOAuthService
{
    Task<ResponseCreateLoginDto> AddGoogleUserAsync(GoogleProfileDto dto);
}