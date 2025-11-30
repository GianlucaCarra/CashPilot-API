using CashPilot.Application.Services;
using CashPilot.Domain.DTOs.Logins.Request;

namespace CashPilot.Application.UseCases.Logins.Commands;

public class ForgotPasswordUseCase
{
    private readonly LoginService _loginService;

    public ForgotPasswordUseCase(LoginService loginService)
    {
        _loginService = loginService;
    }

    public async Task Execute(ForgotPasswordDto dto)
    {
        await _loginService.ForgotPasswordAsync(dto.Email);
    }
}