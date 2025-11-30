namespace CashPilot.Domain.DTOs.Logins.Request;

public class CreateLoginDto
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}