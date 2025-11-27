using Microsoft.Extensions.Caching.Distributed;

namespace CashPilot.Application.Services.Caching;

public class ResetPasswordAttemptService
{
    private readonly IDistributedCache _cache;

    public ResetPasswordAttemptService(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task<int> IncrementAttemptAsync(string email)
    {
        var key = $"reset_password_attempts_{email}";
        var value = await _cache.GetStringAsync(key);
        var count = value is null ? 0 : int.Parse(value);
        
        count++;
        
        await _cache.SetStringAsync(
            key, 
            count.ToString(),
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60)
            });
        
        return count;
    }
    
    public async Task ResetAttemptsAsync(string email)
    {
        var key = $"reset_password_attempts_{email}";
        await _cache.RemoveAsync(key);
    }
}