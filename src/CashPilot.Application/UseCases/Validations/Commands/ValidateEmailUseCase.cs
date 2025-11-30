using CashPilot.Application.Services;
using FluentValidation.Validators;

namespace CashPilot.Application.UseCases.Validations.Commands;

public class ValidateEmailUseCase
{
    private readonly VerificationService _verificationService;

    public ValidateEmailUseCase(VerificationService verificationService)
    {
        _verificationService = verificationService;
    }

    public async Task Execute(string token)
    {
        await _verificationService.VerifyEmailAsync(token);
    }
}