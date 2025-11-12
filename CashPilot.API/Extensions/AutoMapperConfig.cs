using AutoMapper;
using CashPilot.Application.Mappings;

namespace CashPilot.Extensions;

public static class AutoMapperConfig
{
    public static IServiceCollection AddMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg => {},
            typeof(UserProfile));
        
        return services;
    }
}