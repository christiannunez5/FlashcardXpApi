using AutoMapper;
using Domain.Entities.Auth;

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