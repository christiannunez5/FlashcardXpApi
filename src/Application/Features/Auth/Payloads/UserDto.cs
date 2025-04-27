using AutoMapper;
using Domain.Entities.Auth;
using Domain.Entities.Users;

namespace Application.Features.Auth.Payloads;

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