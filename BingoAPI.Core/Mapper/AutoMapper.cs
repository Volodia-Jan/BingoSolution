using AutoMapper;
using BingoAPI.Core.Dtos;
using BingoAPI.Core.Entities;

namespace BingoAPI.Core.Mapper;
public class AutoMapper : Profile
{
    public AutoMapper()
    {
        CreateMap<ApplicationUser, UserResponse>();
        CreateMap<RegisterDto, ApplicationUser>()
            .ForMember(dest => dest.UserName, option => option.MapFrom(src => src.Email))
            .ForMember(dest => dest.Email, option => option.MapFrom(src => src.Email))
            .ForMember(dest => dest.FullName, option => option.MapFrom(src => src.FullName));
    }
}

