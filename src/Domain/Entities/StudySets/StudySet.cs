using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Auth;
using Domain.Entities.Flashcards;
using Domain.Entities.Folders;
using Domain.Entities.Groups;
using Domain.Entities.Tags;
using Domain.Entities.Users;

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
    
   
    // navigations
    public required string CreatedById { get; set; }
    public User CreatedBy { get; set; } = null!;
    
    public ICollection<Flashcard> Flashcards { get; set; } = new List<Flashcard>();
    
    public ICollection<RecentStudySet> RecentStudySets { get; set; } =
        new List<RecentStudySet>();
    public ICollection<GroupStudySet> GroupStudySets { get; set; } = new List<GroupStudySet>();
    
    public ICollection<StudySetParticipant> StudySetParticipants { get; set; } = new List<StudySetParticipant>();
    
    public ICollection<StudySetRating> StudySetRatings { get; set; } = new List<StudySetRating>();
    
    public string? FolderId { get; set; }
    
    public Folder? Folder { get; init; }
    public ICollection<StudySetRecord> StudySetRecords { get; set; } =
        new List<StudySetRecord>();
    
    public ICollection<StudySetTags> StudySetTags { get; set; } = new List<StudySetTags>();
    
    // methods
    public double AverageRating()
    {
        if (StudySetRatings.Count == 0) return 0;
        return StudySetRatings.Average(r => r.Rating);
    }
    
    [NotMapped]
    public int FlashcardsCount => Flashcards.Count;
    
    public void AddTag(Tag tag)
    {
        var newStudySetTag = new StudySetTags
        {
            TagId = tag.Id,
            StudySetId = Id,
        };
        StudySetTags.Add(newStudySetTag);
    }
}