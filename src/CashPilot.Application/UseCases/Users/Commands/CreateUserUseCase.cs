using CashPilot.Application.Services;
using CashPilot.Application.Validators.Users.Commands;
using CashPilot.Domain.DTOs.Users.Request;
using CashPilot.Domain.DTOs.Users.Response;
using FluentValidation;

namespace CashPilot.Application.UseCases.Users.Commands;

public class CreateUserUseCase
{
    private readonly UserService _userService;
    private readonly IValidator<CreateUserDto> _validator;
    
    public CreateUserUseCase(UserService service,  IValidator<CreateUserDto> validator)
    {
        _userService = service;
        _validator = validator;
    }
    
    public async Task<ResponseCreateUserDto> Execute(CreateUserDto dto)
    {
        var result = await _validator.ValidateAsync(dto);

        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors);
        }
        
        return await _userService.AddUserAsync(dto);
    }
}