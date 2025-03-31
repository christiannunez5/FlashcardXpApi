namespace FlashcardXpApi.Domain
{
    public class RecentStudySet
    {
        public string StudySetId { get; set; } = string.Empty;
        public StudySet? StudySet { get; set; }
        public string UserId { get; set; } = string.Empty;
        public User? User { get; set; }

        public DateTime AccessedAt { get; set; } = DateTime.UtcNow;


    }
}
