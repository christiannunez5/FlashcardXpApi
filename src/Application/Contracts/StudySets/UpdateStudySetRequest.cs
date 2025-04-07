using FlashcardXpApi.Application.Contracts.Flashcards;

namespace FlashcardXpApi.Application.Contracts.StudySets
{
    public record UpdateStudySetRequest
    (
        string Title,
        string Description,
        List<UpdateFlashcardRequest> Flashcards
    );
}
