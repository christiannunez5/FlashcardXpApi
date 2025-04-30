using Application.Features.Auth.Payloads;
using AutoMapper;
using Domain.Entities.Studysets;

namespace Application.Features.StudySets.Payloads;

public class StudySetBriefDto
{
    public required string Id { get; init; }
    public required string Title { get; init; }
    public DateOnly UpdatedAt { get; init; }
    public required string Status { get; init; }
    public int FlashcardsCount { get; init; }
    
    public UserDto CreatedBy { get; init; } = null!;
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<StudySet, StudySetBriefDto>()
                .ForMember(dest => dest.Status, src => src.MapFrom(src => src.Status.ToString()));
        }
    }
}
