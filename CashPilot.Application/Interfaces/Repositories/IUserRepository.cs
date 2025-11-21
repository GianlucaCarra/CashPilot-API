using CashPilot.Domain.Entities;

namespace CashPilot.Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User> AddAsync(User user);
    Task<User> FindUserByIdAsync(string id);
    Task<User?> FindUserByEmailAsync(string email);
    Task<bool> ExistsAsync(string email);
    Task<User?> FindUserByTokenAsync(string token);
    Task SaveAsync();
}