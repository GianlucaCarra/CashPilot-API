using CashPilot.Domain.Entities;

namespace CashPilot.Application.Interfaces.Repositories;

public interface IIncomeRepository
{
    Task<Income> AddIncomeAsync(Income income);
    Task<List<Income>> GetAllIncomesAsync(string userId);
    Task<List<Income>> GetIncomesInTimeSpan(DateOnly startDate, DateOnly endDate, string userId);
    Task SaveAsync();
}