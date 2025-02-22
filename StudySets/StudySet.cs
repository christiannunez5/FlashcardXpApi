
using FlashcardXpApi.Users;
using System.ComponentModel.DataAnnotations.Schema;
using FlashcardXpApi.Flashcards;

namespace FlashcardXpApi.FlashcardSets
{
    [Table("StudySet")]
    public class StudySet
    {
        public int Id { get; set; }
        public required string Title  { get; set; }
        public string? Description  { get; set; }

        public DateOnly CreatedAt { get; set; }
        
        // navigations
        public int CreatedById { get; set; }
        public required User CreatedBy { get; set; }

        public ICollection<Flashcard> Flashcards { get; set; } = new List<Flashcard>();
    }
}
