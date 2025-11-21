using AutoMapper;
using CashPilot.Application.Interfaces.Repositories;
using CashPilot.Domain.Exceptions;

namespace CashPilot.Application.Helpers;

public class EmailHelper
{
    private readonly IUserRepository _userRepository;

    public EmailHelper(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task EmailExists(string email)
    {
        var userExists = await _userRepository.ExistsAsync(email);

        if (userExists)
        {
            throw new ConflictException("User with that email already exists");
        }
    }

}