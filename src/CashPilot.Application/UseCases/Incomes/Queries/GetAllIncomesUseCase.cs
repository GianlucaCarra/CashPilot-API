using CashPilot.Application.Services;
using CashPilot.Domain.DTOs.Incomes.Response;

namespace CashPilot.Application.UseCases.Incomes.Queries;

public class GetAllIncomesUseCase
{
    private readonly IncomeService _incomeService;

    public GetAllIncomesUseCase(IncomeService incomeService)
    {
        _incomeService = incomeService;
    }
    
    public async Task<ResponseAllIncomesDto> Execute(string userId)
    {
        return await _incomeService.GetAllIncomes(userId);
    }
}