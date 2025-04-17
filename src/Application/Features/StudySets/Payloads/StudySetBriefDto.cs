using AutoMapper;
using Domain.Entities.Studysets;

namespace Application.Features.StudySets.Payloads;

public class StudySetBriefDto
{
    public required string Id { get; set; }
    public required string Title { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateOnly UpdatedAt { get; set; }
    public required string Status { get; set; }
    public int FlashcardsCount { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<StudySet, StudySetBriefDto>()
                .ForMember(dest => dest.Status, src => src.MapFrom(src => src.Status.ToString()));
        }
    }
}
