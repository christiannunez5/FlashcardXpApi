namespace FlashcardXpApi.Application.Contracts
{
    public record StudySetSummaryResponse(
        string Id,
        string Title,
        string Description,
        DateOnly CreatedAt,
        DateOnly UpdatedAt,
        string Status,
        int FlashcardsCount
    );
    
}
