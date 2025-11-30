using CashPilot.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashPilot.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Income> Incomes { get; set; } = null!;
}