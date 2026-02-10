using CashPilot.Application.Interfaces.Services;
using CashPilot.Application.Services;
using CashPilot.Domain.DTOs.Incomes.Response;

namespace CashPilot.Application.UseCases.Incomes.Queries;

public class GetAllIncomesUseCase
{
    private readonly IIncomeService _incomeService;

    public GetAllIncomesUseCase(IIncomeService incomeService)
    {
        _incomeService = incomeService;
    }
    
    public async Task<List<ResponseIncomeDto>> Execute(string userId)
    {
        return await _incomeService.GetAllIncomes(userId);
    }
}