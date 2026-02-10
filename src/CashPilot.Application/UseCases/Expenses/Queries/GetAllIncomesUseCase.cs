using CashPilot.Application.Interfaces.Services;
using CashPilot.Domain.DTOs.Expenses.Request;
using CashPilot.Domain.DTOs.Expenses.Response;

namespace CashPilot.Application.UseCases.Expenses.Queries;

public class GetAllIncomesUseCase
{
    private readonly IExpenseService _expenseService;
        
    public GetAllIncomesUseCase(IExpenseService expenseService)
    {
        _expenseService = expenseService;
    }

    public async Task<List<ResponseExpenseDto>> Execute(string userId)
    {
        return await _expenseService.GetAllExpenses(userId);
    }
}