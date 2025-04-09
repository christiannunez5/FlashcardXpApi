using AutoMapper;
using Domain.Entities.Auth;
using Domain.Entities.Flashcards;
using Domain.Entities.Studysets;

namespace Application.Common.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        /*
        CreateMap<User, UserResponse>();
        
        CreateMap<Flashcard, FlashcardResponse>();
            
        CreateMap<StudySet, StudySetSummaryResponse>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

        CreateMap<StudySet, StudySetResponse>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.Flashcards, opt => opt.MapFrom(src => src.Flashcards));
            
        */
    }
}

