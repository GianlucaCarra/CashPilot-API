using AutoMapper;
using CashPilot.Domain.DTOs.Logins.Response;
using CashPilot.Domain.DTOs.Users.Request;
using CashPilot.Domain.Entities;

namespace CashPilot.Application.Mappings;

public class LoginProfile : Profile
{
    public LoginProfile()
    {
        CreateMap<User, ResponseCreateLoginDto>()
            .ForMember(dest => dest.Token, opt => opt.Ignore());
    }
}