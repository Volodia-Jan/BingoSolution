using AutoMapper;
using BingoAPI.Core.Dtos;
using BingoAPI.Core.Entities;

namespace BingoAPI.Core.Mapper;
public class AutoMapper : Profile
{
    public AutoMapper()
    {
        CreateMap<ApplicationUser, UserResponse>();
        CreateMap<RegisterDto, ApplicationUser>();
    }
}

