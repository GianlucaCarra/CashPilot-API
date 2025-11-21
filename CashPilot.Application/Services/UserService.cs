using AutoMapper;
using CashPilot.Application.Helpers;
using CashPilot.Application.Interfaces.Repositories;
using CashPilot.Application.Interfaces.Services;
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
    private readonly ITokenService _tokenService;
    private readonly VerificationService _verificationService;

    public UserService(IUserRepository userRepository,  EmailHelper emailHelper, IMapper mapper, ITokenService tokenService, VerificationService verificationService)
    {
        _userRepository = userRepository;
        _emailHelper = emailHelper;
        _mapper = mapper;
        _tokenService = tokenService;
        _verificationService = verificationService;
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
        }
        
        _mapper.Map(dto, entity);
        
        await _userRepository.SaveAsync();
    }

    public async Task<ResponseCreateUserDto> AddUserAsync(CreateUserDto dto)
    {
        await _emailHelper.EmailExists(dto.Email);
        
        var user = _mapper.Map<CreateUserDto, User>(dto);
        
        user.PasswordHash = PasswordHelper.GetPasswordHash(dto.Password);
        user.EmailVerifyToken = await _verificationService.CreateVerificationTokenAsync(user.Email);
        
        await _userRepository.AddAsync(user);
        await _userRepository.SaveAsync();
        
        return _mapper.Map<User, ResponseCreateUserDto>(user);
    }
    
    
}