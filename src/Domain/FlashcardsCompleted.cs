

namespace FlashcardXpApi.Domain
{
    public class FlashcardsCompleted
    {
        public string UserId { get; set; } = string.Empty;
        public User? User { get; set; }
        public string FlashcardId { get; set; } = string.Empty;
        public Flashcard? Flashcard { get; set; }    

        public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);


    }
}
