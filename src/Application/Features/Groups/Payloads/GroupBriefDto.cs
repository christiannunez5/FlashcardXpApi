using Application.Features.Auth.Payloads;
using AutoMapper;
using Domain.Entities.Groups;
using Domain.Entities.Users;

namespace Application.Features.Groups.Payloads;

public class GroupBriefDto
{
    public required string Id { get; set; }
    public string Name { get; init; } = String.Empty;
    public int MembersCount { get; init; }
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Group, GroupBriefDto>()
                .ForMember(dest => dest.MembersCount, opt => opt.MapFrom(src => src.GroupMembers.Count + 1));
        }
    }
}