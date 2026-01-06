using AutoMapper;
using CashPilot.Application.Interfaces.Repositories;
using CashPilot.Application.Interfaces.Services;
using CashPilot.Domain.DTOs.Logins.Response;
using CashPilot.Domain.DTOs.OAuth.Request;
using CashPilot.Domain.DTOs.Users.Request;
using CashPilot.Domain.Entities;
using CashPilot.Domain.Exceptions;

namespace CashPilot.Application.Services;

public class OAuthService : IOAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;

    public OAuthService(
        IUserRepository userRepository, 
        IUserService userService, 
        ITokenService tokenService,
        IMapper mapper) 
    {
        _userRepository = userRepository;
        _userService = userService;
        _tokenService = tokenService;
        _mapper = mapper;
    }
    
    public async Task<ResponseCreateLoginDto> AddGoogleUserAsync(GoogleProfileDto dto)
    {
        var user = _mapper.Map<GoogleProfileDto, User>(dto);
        
        var userExists = await _userRepository.FindUserByEmailAsync(user.Email);

        if (userExists == null)
        {
            var createUserDto = await _userService.AddUserAsync(_mapper.Map<CreateUserDto>(dto));
            return _mapper.Map<ResponseCreateLoginDto>(createUserDto);
        }

        if (!user.Activated)
        {
            throw new BadRequestException("User is not activated");
        }
        
        var token = _tokenService.GenerateToken(user.Id.ToString(), user.Email);
        var response = _mapper.Map<ResponseCreateLoginDto>(user);
        response.Token = token;
        
        return response;
    }
}