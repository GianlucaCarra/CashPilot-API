using CashPilot.Application.Interfaces.Services;
using CashPilot.Application.Services;
using CashPilot.Domain.DTOs.Expenses.Response;
using CashPilot.Domain.DTOs.Incomes.Response;

namespace CashPilot.Application.UseCases.Incomes.Queries;

public class GetAllExpensesUseCase
{
    private readonly IExpenseService _expenseService;

    public GetAllExpensesUseCase(IExpenseService expenseService)
    {
        _expenseService = expenseService;
    }
    
    public async Task<List<ResponseExpenseDto>> Execute(string userId)
    {
        return await _expenseService.GetAllExpenses(userId);
    }
}