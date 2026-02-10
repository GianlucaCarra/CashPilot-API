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
        var entity = await _context.Incomes.AddAsync(income);
        
        return entity.Entity;
    }

    public async Task<List<Income>> GetAllIncomesAsync(string userId)
    {
        var stringUserId = Guid.Parse(userId);
        return await _context.Incomes
            .AsNoTracking()
            .Where(e => e.UserId == stringUserId)
            .ToListAsync(); ;
    }

    public async Task SaveAsync()
    {
        await  _context.SaveChangesAsync();
    }
}