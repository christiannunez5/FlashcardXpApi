using System.Collections;
using Domain.Entities.Flashcards;
using Domain.Entities.Folders;
using Domain.Entities.Groups;
using Domain.Entities.Studysets;
using Domain.Entities.UserExperiences;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Users;

public class User : IdentityUser
{
    public string ProfilePicUrl { get; set; } = default!;
    
    public ICollection<StudySet> StudySets { get; set; } = new List<StudySet>();
    
    public ICollection<CompletedFlashcard> CompletedFlashcards { get; set; } =
        new List<CompletedFlashcard>();
    
    public ICollection<RecentStudySet> RecentStudySets { get; set; } =
        new List<RecentStudySet>();
    
    public ICollection<GroupMembers> GroupMembers { get; set; } =
        new List<GroupMembers>();
        
    public ICollection<StudySetRating> StudySetRatings { get; init; } = new List<StudySetRating>();

    /*
    public ICollection<StudySetRecord> StudySetRecords { get; set; } = new List<StudySetRecord>();
    */
    
    public UserExperience Experience { get; set; } = null!;
    public ICollection<StudySetParticipant> StudySetParticipants { get; set; } = new List<StudySetParticipant>();

    public ICollection<Folder> Folders = new List<Folder>();


}