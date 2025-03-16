namespace FlashcardXpApi.Application.Contracts
{
    public record UserQuestResponse
    (
        string Id,
        string Title,
        string Description,
        bool IsComplete,
        int XpReward
    );
    
}
