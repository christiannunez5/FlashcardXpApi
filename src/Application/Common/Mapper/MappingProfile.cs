using AutoMapper;
using FlashcardXpApi.Application.Contracts;
using FlashcardXpApi.Domain;

namespace FlashcardXpApi.Application.Common.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserResponse>();

            CreateMap<Flashcard, FlashcardResponse>();
            
            CreateMap<StudySet, StudySetSummaryResponse>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<StudySet, StudySetResponse>()
               .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
               .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
               .ForMember(dest => dest.Flashcards, opt => opt.MapFrom(src => src.Flashcards));


        }
    }
}
