﻿

using Application.Features.Auth.Payloads;
using Application.Features.Users.Payloads;
using AutoMapper;
using Domain.Entities.UserExperiences;

namespace Application.Features.UserExperiences.Payloads;


public class UserExperienceDto
{
    public UserDto User { get; set; } = default!;
    public int CurrentExperience { get; set; }
    public LevelDto Level { get; set; } = default!;
    
    public int MaxXp { get; set; }
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<UserExperience, UserExperienceDto>()
                .ForMember(dest => dest.Level, opt => opt.MapFrom(src =>
                    new LevelDto((int) src.GetLevel, src.GetLevel.ToString())
                ))
                .ForMember(dest => dest.CurrentExperience, opt => opt.MapFrom(src => src.Xp))
                .ForMember(dest => dest.MaxXp, opt => opt.MapFrom(src => src.GetMaxXp));
        }
    }
}
