using AutoMapper;
using CashPilot.Application.Helpers;
using CashPilot.Application.Interfaces.Repositories;
using CashPilot.Application.Interfaces.Services;
using CashPilot.Application.Interfaces.Services.Caching;
using CashPilot.Application.Services.Caching;
using CashPilot.Domain.DTOs.Logins.Response;
using CashPilot.Domain.Exceptions;

namespace CashPilot.Application.Services;

public class LoginService : ILoginService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IEmailService _emailService;
    private readonly ILoginAttemptService _loginAttemptService;
    private readonly IMapper _mapper;

    public LoginService(
        IUserRepository userRepository, 
        ITokenService tokenService, 
        IEmailService emailService, 
        ILoginAttemptService loginAttemptService,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _emailService = emailService;
        _loginAttemptService = loginAttemptService;
        _mapper = mapper;
    }
    
    public async Task<ResponseCreateLoginDto> LogUserAsync(string email, string password)
    {
        var entity = await _userRepository.FindUserByEmailAsync(email);

        if (entity is null)
        {
            await Task.Delay(500);
            throw new BadRequestException("Invalid Password or E-mail");
        }
        
        var count = await _loginAttemptService.IncrementAttemptAsync(email);

        if (count > 8)
        {
            throw new BadRequestException("Invalid Password or E-mail");
        }
        
        var passwordIsValid = PasswordHelper.ComparePassword(entity.PasswordHash, password);

        if (!passwordIsValid)
        {
            throw new BadRequestException("Invalid Password or E-mail");
        }
        
        await _loginAttemptService.ResetAttemptsAsync(email);

        if (!entity.Activated)
        {
            throw new BadRequestException("User is not activated");
        }

        var token = _tokenService.GenerateToken(entity.Id.ToString(), email);
        
        var response = _mapper.Map<ResponseCreateLoginDto>(entity);
        response.Token = token;
        
        return response;
    }

    public async Task ForgotPasswordAsync(string email)
    {
        var entity = await _userRepository.FindUserByEmailAsync(email);

        if (entity is null)
        {
            await Task.Delay(500);
            return;
        }
        
        entity.PasswordResetToken = _tokenService.GenerateToken(entity.Id.ToString(), email);
        
        await _emailService.SendResetPasswordEmailAsync(entity.Name, email, entity.PasswordResetToken);
        await _userRepository.SaveAsync();
    }
}