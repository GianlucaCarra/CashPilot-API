using CashPilot.Application.Services.Caching;

namespace CashPilot.Extensions;

public static class RedisCacheConfig
{
    public static IServiceCollection AddRedisCache(this IServiceCollection services,  ConfigurationManager configuration)
    {
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("Redis");
        });

        services.AddScoped<LoginAttemptService>();
        services.AddScoped<ResetPasswordAttemptService>();
        
        return services;
    }
}