using System.Text.Json;
using StackExchange.Redis;

namespace CashPilot.Infrastructure.Caching;

public class RedisCachingService
{
    private readonly IDatabase _database;

    public RedisCachingService(IConnectionMultiplexer connection)
    {
        _database = connection.GetDatabase();
    }

    public Task<bool> SetAsync(string key, string value, TimeSpan? expiry = null)
        => _database.StringSetAsync(key, value, expiry,  When.Always, CommandFlags.None);
    
    public async Task<T> GetAsync<T>(string key)
    {
        var value = await _database.StringGetAsync(key);
        return (value.HasValue ? JsonSerializer.Deserialize<T>(value!) : default)!;
    }

    public Task<bool> ExistAsync(string key)
        => _database.KeyExistsAsync(key);

    public Task<bool> DeleteAsync(string key)
        => _database.KeyDeleteAsync(key);
    
    public Task<long> IncrementAsync(string key)
        => _database.StringIncrementAsync(key);

    public Task<long> DecrementAsync(string key)
        => _database.StringDecrementAsync(key);
}