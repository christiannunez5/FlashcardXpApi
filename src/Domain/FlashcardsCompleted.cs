

namespace FlashcardXpApi.Domain
{
    public class FlashcardsCompleted
    {
        public string UserId { get; set; } = string.Empty;
        public required User User { get; set; }
        public string FlashcardId { get; set; } = string.Empty;
        public required Flashcard Flashcard { get; set; }    

        public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);


    }
}
