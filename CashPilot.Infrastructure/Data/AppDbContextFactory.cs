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

        if (isPostgres)
        {
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("PostgresConnection"));
        }
        else
        {
            optionsBuilder.UseSqlite(configuration.GetConnectionString("SqliteConnection"));
        }
        
        return new AppDbContext(optionsBuilder.Options);
    }
}