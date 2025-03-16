namespace FlashcardXpApi.Application.Contracts
{
    public record StudySetResponse(
        string Id,
        string Title,
        string? Description,
        UserResponse CreatedBy,
        DateOnly CreatedAt,
        DateOnly UpdatedAt,
        string Status,
        List<FlashcardResponse> Flashcards
    );
    
}
