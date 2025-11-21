using AutoMapper;
using CashPilot.Application.Helpers;
using CashPilot.Application.Interfaces.Repositories;
using CashPilot.Application.Interfaces.Services;
using CashPilot.Domain.Exceptions;

namespace CashPilot.Application.Services;

public class VerificationService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;
    private readonly EmailService _emailService;
    
    public VerificationService(IUserRepository userRepository, IMapper mapper, ITokenService tokenService, EmailService emailService)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _tokenService = tokenService;
        _emailService = emailService;
    }

    public async Task<string> CreateVerificationTokenAsync(string email)
    {
        var token = _tokenService.GenerateVerificationToken(email);
        
        await _emailService.SendVerificationEmail(email, token);
        
        return token;
    }
    
    public async Task VerifyEmailAsync(string token)
    {
        var entity = await _userRepository.FindUserByTokenAsync(token);

        if (entity?.EmailVerifyToken is null)
        {
            throw new BadRequestException("E-mail not verified");
        }

        var isValid = _tokenService.ValidateVerificationToken(token);

        if (!isValid)
        {
            throw new BadRequestException("E-mail not verified");
        }
        
        entity.Activated = true;
        entity.EmailVerifyToken = null;

        await _userRepository.SaveAsync();
    }
}