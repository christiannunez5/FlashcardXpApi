using Domain.Entities.Auth;
using Domain.Entities.Flashcards;
using Domain.Entities.Groups;
using Domain.Entities.Quests;
using Domain.Entities.Studysets;
using Domain.Entities.UserExperiences;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Abstraction;

public interface IApplicationDbContext
{
    // study sets
    DbSet<StudySet> StudySets { get; }
    DbSet<RecentStudySet> RecentStudySets { get; }
    DbSet<GroupStudySet> GroupStudySets { get; }
    DbSet<StudySetParticipant> StudySetParticipants { get; }   
    DbSet<StudySetRating> StudySetRatings { get; }
    
    // flashcards
    DbSet<Flashcard> Flashcards { get; }
    DbSet<CompletedFlashcard> CompletedFlashcards { get; }
    
    // quests
    DbSet<Quest> Quests { get; }
    DbSet<UserQuest> UserQuests { get; }
    
    // user experience
    DbSet<UserExperience> UserExperiences { get; }
    
    // groups
    DbSet<Group> Groups { get; }
    
    // auth
    DbSet<RefreshToken> RefreshTokens { get; }

    
    

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}