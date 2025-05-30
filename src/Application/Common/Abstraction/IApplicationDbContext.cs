using Domain.Entities.Auth;
using Domain.Entities.Flashcards;
using Domain.Entities.Folders;
using Domain.Entities.Groups;
using Domain.Entities.Quests;
using Domain.Entities.Studysets;
using Domain.Entities.Tags;
using Domain.Entities.UserExperiences;
using Domain.Entities.Users;
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
    DbSet<StudySetRecord> StudySetRecords { get; }
    DbSet<StudySetTags>  StudySetTags { get; }
    
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

    // folders
    DbSet<Folder> Folders { get; }
    
    // users following
    DbSet<UserFollowing> UserFollowings { get; }
    
    // tags
    DbSet<Tag> Tags { get;  }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}