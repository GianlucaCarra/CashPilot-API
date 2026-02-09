using CashPilot.Application.Interfaces.Services;
using CashPilot.Application.Services;
using CashPilot.Domain.DTOs.Logins.Response;

namespace CashPilot.Application.UseCases.Logins.Commands;

public class CreateLoginUseCase
{
    private readonly ILoginService _loginService;

    public CreateLoginUseCase(ILoginService service)
    {
        _loginService = service;
    }
    
    public async Task<ResponseCreateLoginDto> Execute(string email, string password)
    {
        return await _loginService.LogUserAsync(email, password);
    }
}