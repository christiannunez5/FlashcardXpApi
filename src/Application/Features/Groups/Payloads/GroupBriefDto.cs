using Application.Features.Auth.Payloads;
using AutoMapper;
using Domain.Entities.Groups;
using Domain.Entities.Users;

namespace Application.Features.Groups.Payloads;

public class GroupBriefDto
{
    public required string Id { get; set; }
    public string Name { get; init; } = String.Empty;
    public UserDto CreatedBy { get; set; } = null!;
    
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Group, GroupBriefDto>();
        }
    }
}