using System.Security.Claims;
using AutoMapper;
using CashPilot.Domain.DTOs.OAuth.Request;

namespace CashPilot.Application.Mapping;

public class ClaimsProfile : Profile
{
    public ClaimsProfile()
    {
        CreateMap<ClaimsPrincipal, GoogleProfileDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FindFirst(ClaimTypes.Name)!.Value))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FindFirst(ClaimTypes.Email)!.Value))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FindFirst("email_verified")!.Value));
    }
}