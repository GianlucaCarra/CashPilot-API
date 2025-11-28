using CashPilot.Domain.Entities;

namespace CashPilot.Application.Interfaces.Repositories;

public interface IIncomeRepository
{
    Task<Income> AddIncomeAsync(Income income);
    Task SaveAsync();
}