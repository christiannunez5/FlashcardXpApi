using FlashcardXpApi.Application.Contracts.Auth;
using FlashcardXpApi.Application.Contracts.Flashcards;

namespace FlashcardXpApi.Application.Contracts.StudySets
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
