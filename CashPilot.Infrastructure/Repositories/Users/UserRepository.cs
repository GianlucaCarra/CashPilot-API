using CashPilot.Application.Interfaces.Repositories;
using CashPilot.Infrastructure.Data;
using CashPilot.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashPilot.Infrastructure.Repositories.Users;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<User> AddAsync(User user)
    { 
        var entity = await _context.Users.AddAsync(user);
        
        return entity.Entity;
    }

    public async Task<bool> ExistsAsync(string email)
    {
        var userExists = await _context.Users.FirstOrDefaultAsync(u => u.Email == email); 
        
        return userExists != null;
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}