using CashPilot.Domain.DTOs.Incomes.Request;
using CashPilot.Domain.DTOs.Incomes.Response;

namespace CashPilot.Application.Interfaces.Services;

public interface IIncomeService
{
    Task<ResponseAllIncomesDto> GetAllIncomes(string userId);
    Task<ResponseCreateIncomeDto> CreateIncomeAsync(CreateIncomeDto dto, string userId);
}