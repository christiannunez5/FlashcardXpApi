using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FlashcardXpApi.Domain
{
    [Table("Flashcard")]
    public class Flashcard
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public required string Term { get; set; }

        public required string Definition { get; set; }

        public string StudySetId { get; set; } = string.Empty;
        public StudySet? StudySet { get; set; }


        [JsonIgnore]
        public ICollection<FlashcardsCompleted> FlashcardsCompleted { get; set; } =
            new List<FlashcardsCompleted>();

    }
}
