using System.Security.Claims;
using CashPilot.Domain.Entities;

namespace CashPilot.Application.Interfaces.Services;

public interface ITokenService
{
    string GenerateToken(string id, string email);
    string GenerateVerificationToken(string email, int lifetimeInMinutes);
    bool ValidateVerificationToken(string token);
}