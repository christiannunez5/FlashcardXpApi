namespace FlashcardXpApi.Domain
{
    public class StudySetVisitor
    {

        public string UserId { get; set; } = string.Empty;

        public User? User { get; set; }
        public string StudySetId { get; set; } = string.Empty;
        public StudySet? StudySet { get; set; }

    }
}
