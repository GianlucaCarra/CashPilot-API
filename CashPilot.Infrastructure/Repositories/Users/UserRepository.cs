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
    
    public async Task<User> AddAsync(User user)
    { 
        var entity = await _context.Users.AddAsync(user);
        
        return entity.Entity;
    }

    public async Task<User> FindUserByIdAsync(string id)
    {
        var entity = await _context.Users.FindAsync(Guid.Parse(id));

        if (entity is null)
        {
            throw new NotFoundException("User not found");
        }
        
        return entity;
    }
    
    public async Task<User?> FindUserByEmailAsync(string email)
    {
        var entity = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        
        return entity;
    }

    public async Task<bool> ExistsAsync(string email)
    {
        var userExists = await _context.Users.FirstOrDefaultAsync(u => u.Email == email); 
        
        return userExists != null;
    }

    public async Task<User?> FindUserByTokenAsync(string token)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.EmailVerifyToken == token);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}