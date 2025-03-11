using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using FlashcardXpApi.Features.Flashcards;
using FlashcardXpApi.Features.Users;

namespace FlashcardXpApi.Features.StudySets
{
    [Table("StudySet")]
    public class StudySet
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public required string Title { get; set; }
        public string? Description { get; set; }

        public DateOnly CreatedAt { get; set; }

        public bool IsPublic { get; set; }

        // navigations
        public string? CreatedById { get; set; }
        public User? CreatedBy { get; set; }


        public ICollection<Flashcard> Flashcards { get; set; } = new List<Flashcard>();

        public ICollection<StudySetParticipant> StudySetParticipants { get; set; } =
           new List<StudySetParticipant>();


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

        [NotMapped]
        public int FlashcardsCount => Flashcards.Count;
    }
}
