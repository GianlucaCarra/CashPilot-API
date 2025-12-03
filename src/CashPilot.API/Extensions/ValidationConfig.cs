using CashPilot.Application.Validators.Incomes.Commands;
using CashPilot.Application.Validators.Users.Commands;
using FluentValidation;

namespace CashPilot.Extensions;

public static class ValidationConfig
{
    public static IServiceCollection AddValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<CreateUserValidator>();
        services.AddValidatorsFromAssemblyContaining<UpdateUserValidator>();
        services.AddValidatorsFromAssemblyContaining<ResetPasswordValidator>();
        
        services.AddValidatorsFromAssemblyContaining<CreateIncomeValidator>();
        
        return services;
    }
}