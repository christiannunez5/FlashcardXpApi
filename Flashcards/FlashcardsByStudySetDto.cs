using FlashcardXpApi.Users;

namespace FlashcardXpApi.Flashcards
{
    public record FlashcardsByStudySetDto (
        string Id,
        string Title,
        string? Description,
        UserDto CreatedBy,
        List<FlashcardDto> Flashcards
    );
    
}
