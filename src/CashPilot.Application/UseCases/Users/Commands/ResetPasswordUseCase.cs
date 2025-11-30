using CashPilot.Application.Services;
using CashPilot.Domain.DTOs.Users.Request;
using FluentValidation;

namespace CashPilot.Application.UseCases.Users.Commands;

public class ResetPasswordUseCase
{
    private readonly UserService _userService;
    private readonly IValidator<ResetPasswordDto> _validator;

    public ResetPasswordUseCase(UserService userService, IValidator<ResetPasswordDto> validator)
    {
        _userService = userService;
        _validator = validator;
    }

    public async Task Execute(ResetPasswordDto dto, string token)
    {
        var result = await _validator.ValidateAsync(dto);
        
        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors);
        }
        
        await _userService.ResetPasswordAsync(dto, token);
    }
}