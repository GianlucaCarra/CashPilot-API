using CashPilot.Domain.DTOs.Users.Request;
using FluentValidation;

namespace CashPilot.Application.Validators.Users.Commands;

public class ResetPasswordValidator : AbstractValidator<ResetPasswordDto>
{
    public ResetPasswordValidator()
    {
        RuleFor(x => x.Password)
            .MinimumLength(8).WithMessage("Password must have at least 8 characters")
            .Matches("[A-Z]").WithMessage("Password must have an upper case letter")
            .Matches("[a-z]").WithMessage("Password must have a lower case letter")
            .Matches("[0-9]").WithMessage("Password must have a number")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must have a special character");
    }
}