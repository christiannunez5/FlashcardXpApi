using System.Text.Json.Serialization;
using Domain.Entities.Flashcards;
using Domain.Entities.Studysets;
using Domain.Entities.UserExperiences;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Auth;

public class User : IdentityUser
{
    public string ProfilePicUrl { get; set; } = default!;
    
    public ICollection<StudySet> StudySets { get; set; } = new List<StudySet>();
    
    public ICollection<CompletedFlashcard> CompletedFlashcards { get; set; } =
        new List<CompletedFlashcard>();
    
    public ICollection<RecentStudySet> RecentStudySets { get; set; } =
        new List<RecentStudySet>();

    public UserExperience Experience { get; set; } = null!;
}