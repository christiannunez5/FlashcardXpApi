using Application.Features.Auth.Payloads;
using Application.Features.Users.Payloads;
using AutoMapper;
using Domain.Entities.Studysets;

namespace Application.Features.StudySets.Payloads;

public class RecentStudySetDto
{
    public required string Id { get; init; }
    public required string Title { get; init; }
    public DateTime AccessedAt { get; init; }

    public required UserDto CreatedBy { get; init; }
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<RecentStudySet, RecentStudySetDto>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.StudySet.Title))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.StudySet.Id))
                .ForMember(dest => dest.AccessedAt, opt => opt.MapFrom(src => src.AccessedAt))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.StudySet.CreatedBy));
        }
    }
}