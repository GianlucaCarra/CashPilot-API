using CashPilot.Domain.Entities;

namespace CashPilot.Application.Interfaces.Repositories;

public interface IIncomeRepository
{
    Task<Income> AddIncomeAsync(Income income);
    Task<List<Income>> GetAllIncomesAsync(string userId);
    Task SaveAsync();
}