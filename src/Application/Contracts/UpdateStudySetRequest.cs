namespace FlashcardXpApi.Application.Contracts
{
    public record UpdateStudySetRequest
    (
        string Title,
        string Description,
        List<UpdateFlashcardRequest> Flashcards
    );
}
