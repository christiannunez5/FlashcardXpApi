using AutoMapper;
using FlashcardXpApi.Features.Auth;
using FlashcardXpApi.Features.Flashcards;
using FlashcardXpApi.Features.StudySets;
using FlashcardXpApi.Features.Users;

namespace FlashcardXpApi.Shared.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

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
