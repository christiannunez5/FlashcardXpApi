using Domain.Entities.Auth;
using Domain.Entities.Users;

namespace Domain.Entities.Flashcards;

public class CompletedFlashcard
{
    public required string UserId { get; set; }
    public User User { get; set; } = null!;
    
    public required string FlashcardId { get; set; }
    public Flashcard Flashcard { get; set; } = null!;
    public DateOnly Date { get; set; }

}