

using Application.Features.Auth.Payloads;
using AutoMapper;
using Domain.Entities.UserExperiences;

namespace Application.Features.UserExperiences.Payloads;

public record UserExperienceDto(UserDto User, int CurrentExperience, LevelDto Level, int MaxXp)
{
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<UserExperience, UserExperienceDto>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ForMember(dest => dest.Level, opt => opt.MapFrom(src =>
                    new LevelDto((int)src.GetLevel, src.GetLevel.ToString())
                ))
                .ForMember(dest => dest.CurrentExperience, opt => opt.MapFrom(src => src.Xp))
                .ForMember(dest => dest.MaxXp, opt => opt.MapFrom(src => src.GetMaxXp));
        }
    }
}
