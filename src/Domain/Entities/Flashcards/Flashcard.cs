using Domain.Entities.Studysets;

namespace Domain.Entities.Flashcards;

public class Flashcard
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string Term { get; set; }
    public required string Definition { get; set; }

    public DateTime CreatedAt { get; set; }
    
    // navigations
    public string? StudySetId { get; set; }
    public StudySet? StudySet { get; set; }
    
    public ICollection<CompletedFlashcard> FlashcardsCompleted { get; set; } = new List<CompletedFlashcard>();
    
}

