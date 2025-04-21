using System.ComponentModel.DataAnnotations.Schema;
using AutoMapper;
using Domain.Entities.Quests;

namespace Application.Features.UserQuests.Payloads;

public class UserQuestDto
{
    public required string Id { get; init; }
    public required string Title { get; init; }
    public required string IconUrl  { get; init; } 
    public string Description { get; init; } = string.Empty;
    public bool IsCompleted { get; init; }   
    public int XpReward { get; init; }
    public int Goal { get; init; }
    
    public int CompletedFlashcards { get; init; }
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<UserQuest, UserQuestDto>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Quest.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Quest.Description))
                .ForMember(dest => dest.XpReward, opt => opt.MapFrom(src => src.Quest.XpReward))
                .ForMember(dest => dest.IconUrl, opt => opt.MapFrom(src => src.Quest.IconUrl))
                .ForMember(dest => dest.Goal, opt => opt.MapFrom((src => src.Quest.Goal)))
                .ForMember(dest => dest.CompletedFlashcards,opt => opt.MapFrom((src => src.CompletedFlashcards)));
        }
    }
}
