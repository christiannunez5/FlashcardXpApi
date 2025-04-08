using System.ComponentModel.DataAnnotations.Schema;


namespace FlashcardXpApi.Domain
{
    [Table("StudySet")]
    public class StudySet
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public DateOnly CreatedAt { get; set; }

        public DateOnly UpdatedAt { get; set; }
        public bool IsPublic { get; set; }

        public StudySetStatus Status { get; set; } = StudySetStatus.Draft;

        [NotMapped]
        public int FlashcardsCount => Flashcards.Count;


        // navigations
        public string? CreatedById { get; set; }
        public User? CreatedBy { get; set; }

        public ICollection<Flashcard> Flashcards { get; set; } = new List<Flashcard>();

        public ICollection<StudySetParticipant> StudySetParticipants { get; set; } =
            new List<StudySetParticipant>();

        public ICollection<RecentStudySet> RecentStudySets { get; set; } =
            new List<RecentStudySet>();

        // functions
        public void AddParticipant(User user)
        {
            StudySetParticipants.Add(new StudySetParticipant { UserId = user.Id, StudySetId = Id });
        }

        public bool IsParticipant(string userId)
        {
            return StudySetParticipants
                .Any(sp => sp.UserId == userId);
        }
    }
    
    public enum StudySetStatus
    {
        Draft,
        Published
    }
}
