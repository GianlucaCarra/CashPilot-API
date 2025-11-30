using CashPilot.Application.Helpers;
using CashPilot.Application.Interfaces.Repositories;
using CashPilot.Application.Services;
using CashPilot.Application.UseCases.Incomes.Commands;
using CashPilot.Application.UseCases.Incomes.Queries;
using CashPilot.Application.UseCases.Logins.Commands;
using CashPilot.Application.UseCases.OAuth.Commands;
using CashPilot.Application.UseCases.Users.Commands;
using CashPilot.Application.UseCases.Validations.Commands;
using CashPilot.Filters;
using CashPilot.Infrastructure.Repositories.Users;

namespace CashPilot.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        // Services
        services.AddScoped<UserService>();
        services.AddScoped<LoginService>();
        services.AddScoped<TokenService>();
        services.AddScoped<VerificationService>();
        services.AddScoped<EmailService>();
        services.AddScoped<EmailTemplateService>();
        services.AddScoped<IncomeService>();
        services.AddScoped<OAuthService>();
        
        // Repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IIncomeRepository, IncomeRepository>();
        
        // Use Cases
        services.AddScoped<CreateUserUseCase>();
        services.AddScoped<UpdateUserUseCase>();
        services.AddScoped<ResetPasswordUseCase>();
        
        services.AddScoped<CreateLoginUseCase>();
        services.AddScoped<ForgotPasswordUseCase>();

        services.AddScoped<ValidateEmailUseCase>();
        services.AddScoped<ResendValidationEmailUseCase>();
        
        services.AddScoped<LogOrCreateGoogleUserUseCase>();
        
        services.AddScoped<CreateIncomeUseCase>();
        services.AddScoped<GetAllIncomesUseCase>();
        
        // Filters
        services.AddControllers(options => options.Filters.Add<GlobalExceptionFilter>());
        
        // Helpers
        services.AddScoped<EmailHelper>(); 
        
        return services;
    }
}