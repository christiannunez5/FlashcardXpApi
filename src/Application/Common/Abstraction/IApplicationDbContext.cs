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
    DbSet<StudySet> StudySets { get; }
    DbSet<RecentStudySet> RecentStudySets { get; }
    DbSet<Flashcard> Flashcards { get; }
    DbSet<RefreshToken> RefreshTokens { get; }
    DbSet<CompletedFlashcard> CompletedFlashcards { get; }
    DbSet<Quest> Quests { get; }
    DbSet<UserQuest> UserQuests { get; }
    DbSet<UserExperience> UserExperiences { get; }
    
    DbSet<Group> Groups { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}