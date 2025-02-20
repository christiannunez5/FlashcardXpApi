using FlashcardXpApi.Users;

namespace FlashcardXpApi.FlashcardSets
{
    public record StudySetDto (
        int Id,
        string Title,
        string Description,
        DateOnly CreatedAt,
        UserDto CreatedBy
    );

}
