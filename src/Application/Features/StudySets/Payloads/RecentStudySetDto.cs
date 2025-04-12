using AutoMapper;
using Domain.Entities.Studysets;

namespace Application.Features.StudySets.Payloads;

public record RecentStudySetDto(string Id, string Title, DateTime AccessedAt)
{
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<RecentStudySet, RecentStudySetDto>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.StudySet.Title))
                .ForMember(dest => dest.AccessedAt, opt => opt.MapFrom(src => src.AccessedAt));
        }
    }
}