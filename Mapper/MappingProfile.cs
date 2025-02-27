using AutoMapper;
using FlashcardXpApi.Auth.Requests;
using FlashcardXpApi.Flashcards;
using FlashcardXpApi.Flashcards.Requests;
using FlashcardXpApi.FlashcardSets;
using FlashcardXpApi.Users;

namespace FlashcardXpApi.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {

            // users
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

            CreateMap<CreateUserRequest, User>();
            CreateMap<UserLoginRequest, User>();
            CreateMap<UserLoginRequest, UserDto>();

            CreateMap<StudySet, StudySetDto>();
                
            
            CreateMap<Flashcard, FlashcardDto>();
            CreateMap<FlashcardRequest, Flashcard>();
        }
    }
}
