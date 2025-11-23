using CashPilot.Application.Interfaces.Services;
using CashPilot.Application.Services;
using CashPilot.Domain.DTOs.Verifications.Request;

namespace CashPilot.Application.UseCases.Validations.Commands;

public class ResendValidationEmailUseCase
{
    private readonly VerificationService _verificationService;

    public ResendValidationEmailUseCase(VerificationService verificationService)
    {
        _verificationService = verificationService;
    }

    public async Task Execute(ResendValidationEmailDto dto)
    {
        await _verificationService.ResendVerificationEmailAsync(dto);
    }
}