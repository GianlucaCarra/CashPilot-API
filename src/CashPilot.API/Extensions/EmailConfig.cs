using CashPilot.Application.Configuration;
using CashPilot.Application.Services;
using Microsoft.Extensions.Options;

namespace CashPilot.Extensions;

public static class EmailConfig
{
    public static IServiceCollection AddEmail(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
        services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<EmailSettings>>().Value);
        services.AddScoped<EmailService>();
        
        return services;
    }
}