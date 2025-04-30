
using Domain.Entities.Studysets;

namespace Domain.Entities.Flashcards;

public class StudiedFlashcard
{
    public required string Id { get; set; }
    public required string StudySetProgressId { get; set; }
    public StudySetProgress StudySetProgress { get; set; } = null!;
    
    public required string FlashcardId { get; set; }
    public Flashcard Flashcard { get; set; } = null!;

}
