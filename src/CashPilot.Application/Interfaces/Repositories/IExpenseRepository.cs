using CashPilot.Domain.Entities;

namespace CashPilot.Application.Interfaces.Repositories;

public interface IExpenseRepository
{
    Task<Expense> AddExpenseAsync(Expense expense);
    Task<List<Expense>> GetAllExpensesAsync(string userId);
    Task SaveAsync();
}