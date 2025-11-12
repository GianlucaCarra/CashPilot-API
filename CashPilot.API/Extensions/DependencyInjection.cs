using CashPilot.Application.Interfaces.Repositories;
using CashPilot.Application.Services.Users;
using CashPilot.Application.UseCases.Users.Commands;
using CashPilot.Infrastructure.Repositories.Users;

namespace CashPilot.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        // Services
        services.AddScoped<UserService>();
        
        
        // Repositories
        services.AddScoped<IUserRepository, UserRepository>();
        
        // Use Cases
        services.AddScoped<CreateUserUseCase>();
        
        return services;
    }
}