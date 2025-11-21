using CashPilot.Domain.DTOs.Users.Request;
using FluentValidation;

namespace CashPilot.Application.Validators.Users.Commands;

public class CreateUserValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MinimumLength(3).WithMessage("Name must have at least 3 characters");
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("E-mail is required")
            .EmailAddress().WithMessage("E-mail must be a valid email address");
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password must have at least 8 characters")
            .Matches("[A-Z]").WithMessage("Password must have a upper case letter")
            .Matches("[a-z]").WithMessage("Password must have a lower case letter")
            .Matches("[0-9]").WithMessage("Password must have a number")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must have a number");
    }
}