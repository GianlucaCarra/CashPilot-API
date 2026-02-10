using AutoMapper;
using CashPilot.Application.Helpers;
using CashPilot.Application.Interfaces.Repositories;
using CashPilot.Application.Interfaces.Services;
using CashPilot.Domain.DTOs.Verifications.Request;
using CashPilot.Domain.Exceptions;
using FluentValidation;

namespace CashPilot.Application.Services;

public class VerificationService : IVerificationService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IEmailService _emailService;
    
    public VerificationService(
        IUserRepository userRepository, 
        ITokenService tokenService, 
        IEmailService emailService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _emailService = emailService;
    }

    public async Task<string> CreateVerificationTokenAsync(string name, string email)
    {
        var verificationToken = _tokenService.GenerateVerificationToken(email, 60 * 24);
        
        await _emailService.SendVerificationEmailAsync(name, email, verificationToken);
        
        return verificationToken;
    }
    
    public async Task ResendVerificationEmailAsync(ResendValidationEmailDto dto)
    {
        var email = dto.Email;
        var token = _tokenService.GenerateVerificationToken(email, 60 * 24);
        
        var user = await _userRepository.FindUserByEmailAsync(email);

        if (user is null)
        {
            throw new NotFoundException("User not found");
        }
        
        user.Activated = false;
        user.EmailVerifyToken = token;
        user.UpdatedAt = DateTime.UtcNow;
        
        await _emailService.SendVerificationEmailAsync(user.Name, email, token);
        
        await _userRepository.SaveAsync();
    }
    
    public async Task VerifyEmailAsync(string token)
    {
        var user = await _userRepository.FindUserByTokenAsync(token);

        if (user?.EmailVerifyToken is null)
        {
            throw new BadRequestException("E-mail not verified");
        }

        var isValid = _tokenService.ValidateVerificationToken(token);

        if (!isValid)
        {
            throw new BadRequestException("E-mail not verified");
        }
        
        user.Activated = true;
        user.EmailVerifyToken = null;
        user.UpdatedAt = DateTime.UtcNow;
        
        await _userRepository.SaveAsync();
        await _emailService.SendWelcomeEmailAsync(user.Name, user.Email);
    }
}