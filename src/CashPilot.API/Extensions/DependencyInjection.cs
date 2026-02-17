using CashPilot.Application.Helpers;
using CashPilot.Application.Interfaces.Helpers;
using CashPilot.Application.Interfaces.Repositories;
using CashPilot.Application.Interfaces.Services;
using CashPilot.Application.Services;
using CashPilot.Application.UseCases.Expenses.Commands;
using CashPilot.Application.UseCases.Incomes.Commands;
using CashPilot.Application.UseCases.Incomes.Queries;
using CashPilot.Application.UseCases.Logins.Commands;
using CashPilot.Application.UseCases.OAuth.Commands;
using CashPilot.Application.UseCases.PdfReport.Commands;
using CashPilot.Application.UseCases.Users.Commands;
using CashPilot.Application.UseCases.Validations.Commands;
using CashPilot.Filters;
using CashPilot.Infrastructure.Repositories.Expenses;
using CashPilot.Infrastructure.Repositories.Incomes;
using CashPilot.Infrastructure.Repositories.Users;

namespace CashPilot.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        // Services
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ILoginService, LoginService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IVerificationService, VerificationService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IEmailTemplateService, EmailTemplateService>();
        services.AddScoped<IIncomeService, IncomeService>();
        services.AddScoped<IOAuthService, OAuthService>();
        services.AddScoped<IExpenseService, ExpenseService>();
        services.AddScoped<IPdfReportService, PdfReportService>();
        
        // Repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IIncomeRepository, IncomeRepository>();
        services.AddScoped<IExpenseRepository, ExpenseRepository>();
        
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
        services.AddScoped<GetAllExpensesUseCase>();
        
        services.AddScoped<CreateExpenseUseCase>();
        services.AddScoped<GetAllExpensesUseCase>();
        
        services.AddScoped<CreatePdfReportUseCase>();
        
        // Filters
        services.AddControllers(options => options.Filters.Add<GlobalExceptionFilter>());
        
        // Helpers
        services.AddScoped<IEmailHelper, EmailHelper>(); 
        
        return services;
    }
}