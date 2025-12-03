using CashPilot.Domain.DTOs.Incomes.Request;
using FluentValidation;

namespace CashPilot.Application.Validators.Incomes.Commands;

public class CreateIncomeValidator : AbstractValidator<CreateIncomeDto>
{
    public CreateIncomeValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Provide a valid description")
            .When(x => x.Description != null);
        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("Date is required")
            .LessThan(DateTime.Now).WithMessage("Date must be in the past");
    }
}