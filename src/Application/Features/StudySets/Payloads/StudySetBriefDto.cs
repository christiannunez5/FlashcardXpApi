using AutoMapper;
using Domain.Entities.Studysets;

namespace Application.Features.StudySets.Payloads;

public record StudySetBriefDto
(
    string Id,
    string Title,
    string Description,
    DateOnly UpdatedAt,
    string Status,
    int FlashcardsCount)
{
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<StudySet, StudySetBriefDto>()
                .ForMember(dest => dest.Status, src => src.MapFrom(src => src.Status.ToString()));
        }
    }
}
