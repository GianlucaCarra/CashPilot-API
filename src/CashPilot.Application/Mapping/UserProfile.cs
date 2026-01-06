using AutoMapper;
using CashPilot.Domain.DTOs.OAuth.Request;
using CashPilot.Domain.DTOs.Users.Request;
using CashPilot.Domain.DTOs.Users.Response;
using CashPilot.Domain.Entities;

namespace CashPilot.Application.Mapping;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserDto, User>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Activated, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Expenses, opt => opt.Ignore())
            .ForMember(dest => dest.Incomes, opt => opt.Ignore())
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
            .ForMember(dest => dest.PasswordChangedAt, opt => opt.Ignore())
            .ForMember(dest => dest.EmailVerifyToken, opt => opt.Ignore())
            .ForMember(dest => dest.PasswordResetToken, opt => opt.Ignore());
        
        CreateMap<UpdateUserDto, User>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Activated, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Expenses, opt => opt.Ignore())
            .ForMember(dest => dest.Incomes, opt => opt.Ignore())
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
            .ForMember(dest => dest.PasswordChangedAt, opt => opt.Ignore())
            .ForMember(dest => dest.EmailVerifyToken, opt => opt.Ignore())
            .ForMember(dest => dest.PasswordResetToken, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForAllMembers(opt =>
                opt.Condition((_, _, srcMember) => srcMember != null));

        CreateMap<GoogleProfileDto, User>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Expenses, opt => opt.Ignore())
            .ForMember(dest => dest.Incomes, opt => opt.Ignore())
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
            .ForMember(dest => dest.PasswordChangedAt, opt => opt.Ignore())
            .ForMember(dest => dest.EmailVerifyToken, opt => opt.Ignore())
            .ForMember(dest => dest.PasswordResetToken, opt => opt.Ignore())
            .ForMember(dest => dest.Activated, opt => opt.MapFrom(src => src.EmailVerified));
        
        CreateMap<GoogleProfileDto, CreateUserDto>()
            .ForMember(dest => dest.Password, opt => opt.Ignore());
        
        CreateMap<User, ResponseCreateUserDto>();
    }
}
