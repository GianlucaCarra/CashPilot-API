using AutoMapper;
using CashPilot.Application.Helpers;
using CashPilot.Application.Interfaces.Repositories;
using CashPilot.Application.Interfaces.Services;
using CashPilot.Domain.DTOs.Verifications.Request;
using CashPilot.Domain.Exceptions;
using FluentValidation;

namespace CashPilot.Application.Services;

public class VerificationService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly EmailService _emailService;
    
    public VerificationService(
        IUserRepository userRepository, 
        ITokenService tokenService, 
        EmailService emailService
        )
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _emailService = emailService;
    }

    public async Task<string> CreateVerificationTokenAsync(string name, string email)
    {
        var token = _tokenService.GenerateVerificationToken(email, 60 * 24);
        
        await _emailService.SendVerificationEmailAsync(name, email, token);
        
        return token;
    }
    
    public async Task ResendVerificationEmailAsync(ResendValidationEmailDto dto)
    {
        var email = dto.Email;
        var token = _tokenService.GenerateVerificationToken(email, 60 * 24);
        
        var entity = await _userRepository.FindUserByEmailAsync(email);

        if (entity is null)
        {
            throw new NotFoundException("User not found");
        }
        
        entity.Activated = false;
        entity.EmailVerifyToken = token;
        entity.UpdatedAt = DateTime.UtcNow;
        
        await _emailService.SendVerificationEmailAsync(entity.Name, email, token);
        
        await _userRepository.SaveAsync();
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
        entity.UpdatedAt = DateTime.UtcNow;
        
        await _userRepository.SaveAsync();
        await _emailService.SendWelcomeEmailAsync(entity.Name, entity.Email);
    }
}