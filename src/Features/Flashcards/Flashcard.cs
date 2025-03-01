using FlashcardXpApi.Features.StudySets;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlashcardXpApi.Features.Flashcards
{
    [Table("Flashcard")]
    public class Flashcard
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public required string Term { get; set; }

        public required string Definition { get; set; }

        public string StudySetId { get; set; } = string.Empty;
        public required StudySet StudySet { get; set; }

    }
}
