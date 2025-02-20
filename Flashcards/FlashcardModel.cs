using FlashcardXpApi.FlashcardSets;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlashcardXpApi.Flashcard
{
    [Table("Flashcard")]
    public class FlashcardModel
    {
        public int Id { get; set; }

        public required string Term { get; set; }

        public required string Definition { get; set; } = string.Empty;

        public int StudySetId { get; set; }
        public required StudySet StudySet { get; set; }

    }
}
