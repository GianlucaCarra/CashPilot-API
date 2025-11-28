using CashPilot.Application.Interfaces.Repositories;
using CashPilot.Domain.Entities;
using CashPilot.Infrastructure.Data;

namespace CashPilot.Infrastructure.Repositories.Users;

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

    public async Task SaveAsync()
    {
        await  _context.SaveChangesAsync();
    }
}