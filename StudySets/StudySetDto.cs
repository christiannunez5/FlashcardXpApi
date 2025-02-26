using FlashcardXpApi.Users;

namespace FlashcardXpApi.FlashcardSets
{
    public record StudySetDto (
        string Id,
        string Title,
        string Description,
        DateOnly CreatedAt,
        UserDto createdBy,
        int FlashcardsCount
    );

}
