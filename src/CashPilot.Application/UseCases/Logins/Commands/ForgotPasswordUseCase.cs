using CashPilot.Application.Interfaces.Services;
using CashPilot.Application.Services;
using CashPilot.Domain.DTOs.Logins.Request;

namespace CashPilot.Application.UseCases.Logins.Commands;

public class ForgotPasswordUseCase
{
    private readonly ILoginService _loginService;

    public ForgotPasswordUseCase(ILoginService loginService)
    {
        _loginService = loginService;
    }

    public async Task Execute(ForgotPasswordDto dto)
    {
        await _loginService.ForgotPasswordAsync(dto.Email);
    }
}