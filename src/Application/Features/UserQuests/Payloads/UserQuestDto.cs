
using AutoMapper;
using Domain.Entities.Quests;

namespace Application.Features.Quests.Payloads;

public record UserQuestDto(string Id, string Title, string Description, bool isCompleted, int XpReward)
{
    private class Mapping : Profile
    {
        public Mapping() 
        { 
            CreateMap<UserQuest,  UserQuestDto>();
        }
    }
}
