using AutoMapper;
using ArcadeApp.Core.Models;
using ArcadeApp.Database.Entities;

namespace ArcadeApp.Backend.Mapping;

public class ApiMappingProfile : Profile
{
    public ApiMappingProfile()
    {
        CreateMap<UserEntity, User>().ReverseMap();
        CreateMap<GameStatEntity, GameStat>().ReverseMap();
    }
}
