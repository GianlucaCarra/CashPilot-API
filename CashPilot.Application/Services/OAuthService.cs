using AutoMapper;
using CashPilot.Application.Interfaces.Repositories;
using CashPilot.Application.Interfaces.Services;
using CashPilot.Domain.DTOs.Logins.Response;
using CashPilot.Domain.DTOs.OAuth.Request;
using CashPilot.Domain.DTOs.Users.Request;
using CashPilot.Domain.Entities;
using CashPilot.Domain.Exceptions;

namespace CashPilot.Application.Services;

public class OAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly UserService _userService;
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;

    public OAuthService(
        IUserRepository userRepository, 
        IMapper mapper, 
        UserService userService, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _userService = userService;
        _tokenService = tokenService;
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