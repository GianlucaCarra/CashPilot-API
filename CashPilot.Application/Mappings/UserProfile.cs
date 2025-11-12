using AutoMapper;
using CashPilot.Domain.DTOs.User;
using CashPilot.Domain.DTOs.Users.Response;
using CashPilot.Domain.Entities;

namespace CashPilot.Application.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserDto, User>()
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
        CreateMap<User, ResponseCreateUserDto>();
    }
}