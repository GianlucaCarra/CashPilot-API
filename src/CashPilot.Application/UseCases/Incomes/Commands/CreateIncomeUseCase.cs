using CashPilot.Application.Interfaces.Repositories;
using CashPilot.Application.Services;
using CashPilot.Domain.DTOs.Incomes.Request;
using CashPilot.Domain.DTOs.Incomes.Response;

namespace CashPilot.Application.UseCases.Incomes.Commands;

public class CreateIncomeUseCase
{
    private readonly IncomeService _incomeService;

    public CreateIncomeUseCase(IncomeService incomeService)
    {
        _incomeService = incomeService;
    }
    
    public async Task<ResponseCreateIncomeDto> Execute(CreateIncomeDto dto, string userId)
    {
        return await _incomeService.CreateIncomeAsync(dto, userId);
    }
}