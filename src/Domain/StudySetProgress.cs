namespace FlashcardXpApi.Domain
{
    public class StudySetProgress
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string StudySetId { get; set; } = string.Empty;
        public StudySet? StudySet { get; set; }
        public string UserId { get; set; } = string.Empty;
        public User? User { get; set; }

        public int FlashcardsCompleted { get; set; } = 0;

        public bool isCompleted => StudySet?.Flashcards.Count == FlashcardsCompleted;

        public DateTime LastStudiedAt { get; set; } = DateTime.UtcNow;
    }
}
