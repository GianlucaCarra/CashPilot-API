using CashPilot.Domain.Enums.Database;

namespace CashPilot.Application.Configuration;

public class DatabaseSettings
{
    public DatabaseTypes DatabaseType { get; set; } = DatabaseTypes.Postgres;
}