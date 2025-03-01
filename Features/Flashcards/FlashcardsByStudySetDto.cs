using FlashcardXpApi.Features.Users;

namespace FlashcardXpApi.Features.Flashcards
{
    public record FlashcardsByStudySetDto(
        string Id,
        string Title,
        string? Description,
        UserDto CreatedBy,
        List<FlashcardDto> Flashcards
    );

}
