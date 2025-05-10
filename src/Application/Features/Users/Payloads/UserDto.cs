using AutoMapper;
using Domain.Entities.Users;

namespace Application.Features.Users.Payloads;

public record UserDto(string Id, string Email, string Username, string ProfilePicUrl)
{
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<User, UserDto>();
        }
    }
}