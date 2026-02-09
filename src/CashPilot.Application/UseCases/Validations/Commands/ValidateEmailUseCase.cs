using CashPilot.Application.Interfaces.Services;
using CashPilot.Application.Services;
using FluentValidation.Validators;

namespace CashPilot.Application.UseCases.Validations.Commands;

public class ValidateEmailUseCase
{
    private readonly IVerificationService _verificationService;

    public ValidateEmailUseCase(IVerificationService verificationService)
    {
        _verificationService = verificationService;
    }

    public async Task Execute(string token)
    {
        await _verificationService.VerifyEmailAsync(token);
    }
}