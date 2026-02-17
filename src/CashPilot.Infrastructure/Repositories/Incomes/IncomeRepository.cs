using CashPilot.Application.Interfaces.Repositories;
using CashPilot.Domain.Entities;
using CashPilot.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CashPilot.Infrastructure.Repositories.Incomes;

public class IncomeRepository :  IIncomeRepository
{
    private readonly AppDbContext _context;

    public IncomeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Income> AddIncomeAsync(Income income)
    {
        var incomeEntry = await _context.Incomes.AddAsync(income);
        
        return incomeEntry.Entity;
    }

    public async Task<List<Income>> GetAllIncomesAsync(string userId)
    {
        var userIdParsed = Guid.Parse(userId);
        
        return await _context.Incomes
            .AsNoTracking()
            .Where(e => e.UserId == userIdParsed)
            .ToListAsync(); ;
    }
    
    public async Task<List<Income>> GetIncomesInTimeSpan(DateOnly startDate, DateOnly endDate, string userId)
    {
        var userIdParsed = Guid.Parse(userId);
        DateTime start = startDate.ToDateTime(TimeOnly.MinValue).Date;
        DateTime end   = endDate.ToDateTime(TimeOnly.MaxValue).Date;
        
        return await _context.Incomes
            .AsNoTracking()
            .Where(e => e.UserId == userIdParsed)
            .Where(e => e.Date >= start && e.Date < end.Date.AddDays(1))
            .ToListAsync();
    }

    public async Task SaveAsync()
    {
        await  _context.SaveChangesAsync();
    }
}