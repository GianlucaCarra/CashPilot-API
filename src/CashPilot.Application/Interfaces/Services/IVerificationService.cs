using System.Security.Claims;
using CashPilot.Domain.DTOs.Users.Request;
using CashPilot.Domain.DTOs.Users.Response;
using CashPilot.Domain.DTOs.Verifications.Request;
using CashPilot.Domain.Entities;

namespace CashPilot.Application.Interfaces.Services;

public interface IVerificationService
{
    Task<string> CreateVerificationTokenAsync(string name, string email);
    Task ResendVerificationEmailAsync(ResendValidationEmailDto dto);
    Task VerifyEmailAsync(string token);
}