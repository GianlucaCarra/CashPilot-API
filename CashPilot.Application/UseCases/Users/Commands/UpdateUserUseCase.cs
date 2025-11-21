using CashPilot.Application.Services;
using CashPilot.Domain.DTOs.Users.Request;
using CashPilot.Domain.DTOs.Users.Response;
using FluentValidation;

namespace CashPilot.Application.UseCases.Users.Commands;

public class UpdateUserUseCase
{
    private readonly UserService _userService;
    private readonly IValidator<UpdateUserDto> _validator;
    
    public UpdateUserUseCase(UserService service,  IValidator<UpdateUserDto> validator)
    {
        _userService = service;
        _validator = validator;
    }
    
    public async Task Execute(UpdateUserDto dto, string id)
    {
        var result = await _validator.ValidateAsync(dto);

        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors);    
        }

        await _userService.PatchUserByIdAsync(dto, id);
    }
}