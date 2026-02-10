using CashPilot.Application.Interfaces.Repositories;
using CashPilot.Infrastructure.Data;
using CashPilot.Domain.Entities;
using CashPilot.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CashPilot.Infrastructure.Repositories.Users;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<User> AddUserAsync(User user)
    { 
        var userEntry = await _context.Users.AddAsync(user);
        
        return userEntry.Entity;
    }

    public async Task<User?> FindUserByIdAsync(string id)
    {
        return await _context.Users.FindAsync(Guid.Parse(id));
    }
    
    public async Task<User?> FindUserByEmailAsync(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        
        return user;
    }

    public async Task<bool> ExistsAsync(string email)
    {
        var userExists = await _context.Users.FirstOrDefaultAsync(u => u.Email == email); 
        
        return userExists != null;
    }

    public async Task<User?> FindUserByTokenAsync(string token)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.EmailVerifyToken == token ||  u.PasswordResetToken == token);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}