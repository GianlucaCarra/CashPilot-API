namespace CashPilot.Application.Helpers;

public class PasswordHelper
{
    public static string GetPasswordHash(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public static bool ComparePassword(string hash, string password)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
}