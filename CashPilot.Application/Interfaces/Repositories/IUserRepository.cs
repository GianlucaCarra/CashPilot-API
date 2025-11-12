using CashPilot.Domain.Entities;

namespace CashPilot.Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User> AddAsync(User user);
    Task<bool> ExistsAsync(string email);
    Task SaveAsync();
}