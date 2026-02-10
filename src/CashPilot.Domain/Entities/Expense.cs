using CashPilot.Domain.Enums.Expenses;

namespace CashPilot.Domain.Entities;

public class Expense
{
    public Guid Id { get; set; } =  Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    
    public string? Description { get; set; }
    public ExpenseCategory Category { get; set; }
    public decimal Amount  { get; set; }
    public DateTime Date { get; set; }
    
    public Guid UserId { get; set; }
    public User User { get; set; }
}