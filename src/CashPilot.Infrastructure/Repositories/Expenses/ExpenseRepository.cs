using CashPilot.Application.Interfaces.Repositories;
using CashPilot.Domain.Entities;
using CashPilot.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CashPilot.Infrastructure.Repositories.Expenses;

public class ExpenseRepository : IExpenseRepository
{
    private readonly AppDbContext _context;
    
    public ExpenseRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<Expense> AddExpenseAsync(Expense expense)
    {
        var expenseEntry = await _context.Expenses.AddAsync(expense);
        
        return expenseEntry.Entity;
    }

    public async Task<List<Expense>> GetAllExpensesAsync(string userId)
    {
        var userIdParsed = Guid.Parse(userId);

        return await _context.Expenses
            .AsNoTracking()
            .Where(e => e.UserId == userIdParsed)
            .ToListAsync();
    }

    public async Task<List<Expense>> GetExpensesInTimeSpan(DateOnly startDate, DateOnly endDate, string userId)
    {
        var userIdParsed = Guid.Parse(userId);
        DateTime start = startDate.ToDateTime(TimeOnly.MinValue).Date;
        DateTime end   = endDate.ToDateTime(TimeOnly.MaxValue).Date;
        
        return await _context.Expenses
            .AsNoTracking()
            .Where(e => e.UserId == userIdParsed)
            .Where(e => e.Date >= start && e.Date < end.Date.AddDays(1))
            .ToListAsync();
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}