namespace FlashcardXpApi.Application.Contracts.Flashcards
{
    public record UpdateFlashcardRequest(string? Id, string Term, string Definition, string StudySetId);
    
}
