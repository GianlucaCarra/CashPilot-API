using CashPilot.Domain.Enums.Expenses;

namespace CashPilot.Domain.DTOs.Expenses.Request;

public class CreateExpenseDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public ExpenseCategory Category { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; } 
}