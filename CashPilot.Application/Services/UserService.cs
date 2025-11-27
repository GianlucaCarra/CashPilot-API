using AutoMapper;
using CashPilot.Application.Helpers;
using CashPilot.Application.Interfaces.Repositories;
using CashPilot.Application.Interfaces.Services;
using CashPilot.Application.Services.Caching;
using CashPilot.Domain.DTOs.Users.Request;
using CashPilot.Domain.DTOs.Users.Response;
using CashPilot.Domain.Entities;
using CashPilot.Domain.Exceptions;

namespace CashPilot.Application.Services;

public class UserService
{
    private readonly IUserRepository _userRepository;
    private readonly EmailHelper _emailHelper;
    private readonly IMapper _mapper;
    private readonly VerificationService _verificationService;
    private readonly ITokenService _tokenService;
    private readonly ResetPasswordAttemptService _resetPasswordAttemptService;

    public UserService(
        IUserRepository userRepository, 
        EmailHelper emailHelper, 
        IMapper mapper, 
        VerificationService verificationService, 
        ITokenService tokenService, ResetPasswordAttemptService resetPasswordAttemptService)
    {
        _userRepository = userRepository;
        _emailHelper = emailHelper;
        _mapper = mapper;
        _verificationService = verificationService;
        _tokenService = tokenService;
        _resetPasswordAttemptService = resetPasswordAttemptService;
    }

    public async Task PatchUserByIdAsync(UpdateUserDto dto, string id)
    {
        if (dto.Email is not null)
        {
            await _emailHelper.EmailExists(dto.Email);
        }

        var entity = await _userRepository.FindUserByIdAsync(id);

        if (dto.Password is not null && dto.NewPassword is not null)
        {
            var passwordIsValid = PasswordHelper.ComparePassword(entity.PasswordHash, dto.Password);

            if (!passwordIsValid)
            {
                throw new BadRequestException("Passwords do not match");
            }
            
            entity.PasswordHash = PasswordHelper.GetPasswordHash(dto.NewPassword);
            entity.PasswordChangedAt = DateTime.UtcNow;
        }
        
        _mapper.Map(dto, entity);
        
        await _userRepository.SaveAsync();
    }

    public async Task ResetPasswordAsync(ResetPasswordDto dto, string token)
    {
        var tokenIsValid = _tokenService.ValidateVerificationToken(token);

        if (!tokenIsValid)
        {
            throw new BadRequestException("Verification token not valid");
        }
        
        
        var entity = await _userRepository.FindUserByTokenAsync(token);

        if (entity is null)
        {
            await Task.Delay(500);
            throw new NotFoundException("User not found");
        }

        var count = await _resetPasswordAttemptService.IncrementAttemptAsync(entity.Email);

        if (count > 3)
        {
            throw new BadRequestException("Reset password attempt count exceeded");
        }

        if (dto.Password != dto.ConfirmPassword)
        {
            throw new BadRequestException("Passwords do not match");
        }

        if (PasswordHelper.ComparePassword(entity.PasswordHash, dto.Password))
        {
            throw new BadRequestException("New Password cannot be the same as the old password");
        }
        
        entity.PasswordHash = PasswordHelper.GetPasswordHash(dto.Password);
        entity.UpdatedAt = DateTime.UtcNow;
        entity.EmailVerifyToken = null;
        entity.PasswordChangedAt = DateTime.UtcNow;
        
        await _userRepository.SaveAsync();
    }

    public async Task<ResponseCreateUserDto> AddUserAsync(CreateUserDto dto)
    {
        await _emailHelper.EmailExists(dto.Email);
        
        var user = _mapper.Map<CreateUserDto, User>(dto);
        
        user.PasswordHash = PasswordHelper.GetPasswordHash(dto.Password);
        user.EmailVerifyToken = await _verificationService.CreateVerificationTokenAsync(user.Name, user.Email);
        
        await _userRepository.AddAsync(user);
        await _userRepository.SaveAsync();
        
        return _mapper.Map<User, ResponseCreateUserDto>(user);
    }
}