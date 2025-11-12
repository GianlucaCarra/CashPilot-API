using AutoMapper;
using CashPilot.Application.Interfaces.Repositories;
using CashPilot.Domain.DTOs.User;
using CashPilot.Domain.DTOs.Users.Response;
using CashPilot.Domain.Entities;

namespace CashPilot.Application.Services.Users;

public class UserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository,  IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<ResponseCreateUserDto> AddUserAsync(CreateUserDto dto)
    {
        var userExists = await _userRepository.ExistsAsync(dto.Email);

        if (userExists)
        {
            throw new Exception("User with that email already exists");
        }

        var user = _mapper.Map<CreateUserDto, User>(dto);
        user.PasswordHash = GetPasswordHash(dto.Password);
        
        await _userRepository.AddAsync(user);
        await _userRepository.SaveAsync();
        
        var responseDto = _mapper.Map<User, ResponseCreateUserDto>(user);
        
        return responseDto;
    }

    private string GetPasswordHash(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
}