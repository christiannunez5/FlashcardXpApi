using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Auth;
using Domain.Entities.Flashcards;

namespace Domain.Entities.Studysets;

public class StudySet
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string Title { get; set; }
    public string Description { get; set; } = string.Empty;
        
    public DateOnly CreatedAt { get; set; }

    public DateOnly UpdatedAt { get; set; }
    public bool IsPublic { get; set; }

    public StudySetStatus Status { get; set; } = StudySetStatus.Draft;

    [NotMapped]
    public int FlashcardsCount => Flashcards.Count;
    
    // navigations
    public required string CreatedById { get; set; }
    public User CreatedBy { get; set; } = null!;
    
    public ICollection<Flashcard> Flashcards { get; set; } = new List<Flashcard>();
    
    public ICollection<RecentStudySet> RecentStudySets { get; set; } =
        new List<RecentStudySet>();
}