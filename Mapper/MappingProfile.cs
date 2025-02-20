using AutoMapper;
using FlashcardXpApi.Auth.Requests;
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
            CreateMap<CreateUserRequest, User>();

            CreateMap<StudySet, StudySetDto>()
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy));
            
                
        }
    }
}
