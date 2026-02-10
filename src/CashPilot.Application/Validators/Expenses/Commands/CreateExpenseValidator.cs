using CashPilot.Domain.DTOs.Expenses.Request;
using CashPilot.Domain.DTOs.Incomes.Request;
using FluentValidation;

namespace CashPilot.Application.Validators.Expenses.Commands;

public class CreateExpenseValidator : AbstractValidator<CreateExpenseDto>
{
    public CreateExpenseValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Description)
            .MaximumLength(200).WithMessage("Description cannot exceed 200 characters");
        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("Date is required")
            .LessThan(DateTime.Now).WithMessage("Date must be in the past");
    }
}