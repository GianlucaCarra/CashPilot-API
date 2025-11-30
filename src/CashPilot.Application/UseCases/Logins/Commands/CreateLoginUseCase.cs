using CashPilot.Application.Services;
using CashPilot.Domain.DTOs.Logins.Response;

namespace CashPilot.Application.UseCases.Logins.Commands;

public class CreateLoginUseCase
{
    private readonly LoginService _loginService;

    public CreateLoginUseCase(LoginService service)
    {
        _loginService = service;
    }
    
    public async Task<ResponseCreateLoginDto> Execute(string email, string password)
    {
        return await _loginService.LogUserAsync(email, password);
    }
}