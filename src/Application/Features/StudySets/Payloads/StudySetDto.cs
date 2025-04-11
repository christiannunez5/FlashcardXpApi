using Application.Features.Auth.Payloads;
using Application.Features.Flashcards.Payloads;
using AutoMapper;
using Domain.Entities.Auth;
using Domain.Entities.Studysets;

namespace Application.Features.StudySets.Payloads;

public record StudySetDto(
    string Id,
    string Title,
    string? Description,
    UserDto CreatedBy,
    DateOnly CreatedAt,
    DateOnly UpdatedAt,
    string Status,
    List<FlashcardDto> Flashcards
)
{
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<StudySet, StudySetDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.Flashcards, opt => opt.MapFrom(src => src.Flashcards))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        }
    }
}
