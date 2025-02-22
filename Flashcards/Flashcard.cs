using FlashcardXpApi.FlashcardSets;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlashcardXpApi.Flashcards
{
    [Table("Flashcard")]
    public class Flashcard
    {
        public int Id { get; set; }

        public required string Term { get; set; }

        public required string Definition { get; set; }

        public int StudySetId { get; set; }
        public required StudySet StudySet { get; set; }

    }
}
