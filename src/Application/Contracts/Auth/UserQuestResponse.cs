namespace FlashcardXpApi.Application.Contracts.Auth
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
