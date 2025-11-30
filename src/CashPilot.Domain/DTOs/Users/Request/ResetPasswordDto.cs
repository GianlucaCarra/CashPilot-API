namespace CashPilot.Domain.DTOs.Users.Request;

public class ResetPasswordDto
{
    public string Password { get; set; } =  string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
}