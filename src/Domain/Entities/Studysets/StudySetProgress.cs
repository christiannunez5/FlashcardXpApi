
using Domain.Entities.Auth;
using Domain.Entities.Flashcards;

namespace Domain.Entities.Studysets;

public class StudySetProgress
{
    public required string Id { get; set; }
    public required string UserId { get; set; }

    public User User { get; set; } = null!;

    public required string StudySetId { get; set; }
    public StudySet StudySet { get; set; } = null!;


    public ICollection<StudiedFlashcard> StudiedFlashcards =
        new List<StudiedFlashcard>();    

}
