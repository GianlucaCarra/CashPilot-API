using System.Security.Claims;
using AutoMapper;
using CashPilot.Application.Interfaces.Services;
using CashPilot.Application.Services;
using CashPilot.Domain.DTOs.Logins.Response;
using CashPilot.Domain.DTOs.OAuth.Request;

namespace CashPilot.Application.UseCases.OAuth.Commands;

public class LogOrCreateGoogleUserUseCase
{
    private readonly IOAuthService _oAuthService;
    private readonly IMapper _mapper;

    public LogOrCreateGoogleUserUseCase(IOAuthService oAuthService, IMapper mapper)
    {
        _oAuthService = oAuthService;
        _mapper = mapper;
    }

    public async Task<ResponseCreateLoginDto> Execute(ClaimsPrincipal claims)
    {
        var dto = _mapper.Map<GoogleProfileDto>(claims);
        
        return await _oAuthService.AddGoogleUserAsync(dto);
    } 
}