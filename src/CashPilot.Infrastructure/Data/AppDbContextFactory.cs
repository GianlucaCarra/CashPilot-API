using CashPilot.Domain.Enums.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CashPilot.Infrastructure.Data;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../CashPilot.API"))
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .Build();
        
        var isPostgres = bool.Parse(configuration.GetSection("UsePostgres").Value ?? "false");

        switch (isPostgres)
        {
            case true:
                optionsBuilder.UseNpgsql(configuration.GetConnectionString("PostgresConnection"));
                break;
            case false:
                optionsBuilder.UseSqlite(configuration.GetConnectionString("SqliteConnection"));
                break;
        }
        
        return new AppDbContext(optionsBuilder.Options);
    }
}