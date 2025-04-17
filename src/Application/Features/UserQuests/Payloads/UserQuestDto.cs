
using AutoMapper;
using Domain.Entities.Quests;

namespace Application.Features.Quests.Payloads;



public class UserQuestDto
{
    public required string Id { get; set; }
    public required string Title { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }   
    public int XpReward { get; set; }

    private class Mapping : Profile
    {
        public Mapping() 
        {
            CreateMap<UserQuest, UserQuestDto>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Quest.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Quest.Description))
                .ForMember(dest => dest.XpReward, opt => opt.MapFrom(src => src.Quest.XpReward));
        }
    }
}
