using System.Text.RegularExpressions;
using Application.Common.Abstraction;
using Domain.Entities.Auth;
using Domain.Entities.Flashcards;
using Domain.Entities.Folders;
using Domain.Entities.Quests;
using Domain.Entities.Studysets;
using Domain.Entities.UserExperiences;
using Domain.Entities.Users;
using Infrastructure.Persistence.Configurations;
using Infrastructure.Persistence.Configurations.Flashcards;
using Infrastructure.Persistence.Configurations.Folders;
using Infrastructure.Persistence.Configurations.Quests;
using Infrastructure.Persistence.Configurations.Studysets;
using Infrastructure.Persistence.Configurations.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Group = Domain.Entities.Groups.Group;

namespace Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<User>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    // study sets
    public DbSet<StudySet> StudySets => Set<StudySet>();
    public DbSet<RecentStudySet> RecentStudySets => Set<RecentStudySet>();
    public DbSet<GroupStudySet> GroupStudySets => Set<GroupStudySet>();
    public DbSet<StudySetParticipant> StudySetParticipants => Set<StudySetParticipant>();
    public DbSet<StudySetRating> StudySetRatings => Set<StudySetRating>();
    
    // auth
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
 
    // flashcards
    public DbSet<Flashcard> Flashcards => Set<Flashcard>();
    public DbSet<CompletedFlashcard> CompletedFlashcards => Set<CompletedFlashcard>();
    
    // quests
    public DbSet<Quest> Quests => Set<Quest>();
    public DbSet<UserQuest> UserQuests => Set<UserQuest>();
    
    
    // user experiences
    public DbSet<UserExperience> UserExperiences => Set<UserExperience>();
    
    // groups
    public DbSet<Group> Groups => Set<Group>();
    
    // folders
    public DbSet<Folder> Folders => Set<Folder>();
    
    // user following
    public DbSet<UserFollowing> UserFollowings => Set<UserFollowing>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfiguration(new StudySetConfiguration());
        modelBuilder.ApplyConfiguration(new GroupStudySetConfiguration());
        modelBuilder.ApplyConfiguration(new RecentStudySetConfiguration());
        modelBuilder.ApplyConfiguration(new StudySetParticipantConfiguration());
        modelBuilder.ApplyConfiguration(new StudySetRatingConfiguration());
        
        modelBuilder.ApplyConfiguration(new GroupMemberConfiguration());

        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());
        
        modelBuilder.ApplyConfiguration(new UserQuestConfiguration());
        modelBuilder.ApplyConfiguration(new QuestConfiguration());
        
        modelBuilder.ApplyConfiguration(new UserExperienceConfiguration());
        
        modelBuilder.ApplyConfiguration(new CompletedFlashcardConfiguration());
        modelBuilder.ApplyConfiguration(new FlashcardConfiguration());

        modelBuilder.ApplyConfiguration(new FolderConfiguration());
        // modelBuilder.ApplyConfiguration(new StudySetRecordConfiguration());
        // modelBuilder.ApplyConfiguration(new StudySetProgressConfiguration());
    }


}