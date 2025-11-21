using AutoMapper;
using CashPilot.Application.Helpers;
using CashPilot.Application.Interfaces.Repositories;
using CashPilot.Domain.DTOs.Logins.Response;
using CashPilot.Domain.Exceptions;

namespace CashPilot.Application.Services;

public class LoginService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly TokenService _tokenService;

    public LoginService(IUserRepository userRepository, 
        IMapper mapper,
        TokenService tokenService)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _tokenService = tokenService;
    }
    
    public async Task<ResponseCreateLoginDto> LogUserAsync(string email, string password)
    {
        var entity = await _userRepository.FindUserByEmailAsync(email);

        if (entity is null)
        {
            throw new BadRequestException("Invalid Password or E-mail");
        }
        
        var passwordIsValid = PasswordHelper.ComparePassword(entity.PasswordHash, password);

        if (!passwordIsValid)
        {
            throw new BadRequestException("Invalid Password or E-mail");
        }

        if (!entity.Activated)
        {
            throw new BadRequestException("User is not activated");
        }

        var token = _tokenService.GenerateToken(entity.Id.ToString(), email);
        
        var response = _mapper.Map<ResponseCreateLoginDto>(entity);
        response.Token = token;
        
        return response;
    }
}