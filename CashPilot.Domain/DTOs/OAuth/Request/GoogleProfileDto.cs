namespace CashPilot.Domain.DTOs.OAuth.Request;

public class GoogleProfileDto
{
    public string Sub { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Name { get; set; }
    public bool EmailVerified { get; set; }
}