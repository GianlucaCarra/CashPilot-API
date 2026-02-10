using CashPilot.Domain.DTOs.Expenses.Request;
using CashPilot.Domain.DTOs.Expenses.Response;

namespace CashPilot.Application.Interfaces.Services;

public interface IExpenseService
{
    Task<List<ResponseExpenseDto>> GetAllExpenses(string userId);
    Task<ResponseCreateExpenseDto> CreateExpenseAsync(CreateExpenseDto dto, string userId);
}