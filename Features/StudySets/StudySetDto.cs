
namespace FlashcardXpApi.Features.StudySets
{
    public record StudySetDto(
        string Id,
        string Title,
        string Description,
        DateOnly CreatedAt,
        int FlashcardsCount
    );

}
