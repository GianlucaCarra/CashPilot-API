using System.Security.Claims;
using CashPilot.Domain.DTOs.Users.Request;
using CashPilot.Domain.DTOs.Users.Response;
using CashPilot.Domain.Entities;

namespace CashPilot.Application.Interfaces.Services;

public interface IUserService
{
    Task PatchUserByIdAsync(UpdateUserDto dto, string id);
    Task ResetPasswordAsync(ResetPasswordDto dto, string token);
    Task<ResponseCreateUserDto> AddUserAsync(CreateUserDto dto);
}