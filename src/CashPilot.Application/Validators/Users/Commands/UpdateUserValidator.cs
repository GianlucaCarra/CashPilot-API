using CashPilot.Domain.DTOs.Users.Request;
using FluentValidation;

namespace CashPilot.Application.Validators.Users.Commands;

public class UpdateUserValidator : AbstractValidator<UpdateUserDto>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.Name)
            .MinimumLength(3).WithMessage("Name must have at least 3 characters")
            .When(x => x.Name != null);

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("E-mail must be a valid email address")
            .When(x => x.Email != null);

        RuleFor(x => x.NewPassword)
            .MinimumLength(8).WithMessage("Password must have at least 8 characters")
            .Matches("[A-Z]").WithMessage("Password must have an upper case letter")
            .Matches("[a-z]").WithMessage("Password must have a lower case letter")
            .Matches("[0-9]").WithMessage("Password must have a number")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must have a special character")
            .When(x => x.NewPassword != null);
    }
}