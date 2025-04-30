using System.Text.RegularExpressions;
using Application.Common.Abstraction;
using Domain.Entities.Auth;
using Domain.Entities.Flashcards;
using Domain.Entities.Quests;
using Domain.Entities.Studysets;
using Domain.Entities.UserExperiences;
using Domain.Entities.Users;
using Infrastructure.Persistence.Configurations;
using Infrastructure.Persistence.Configurations.Flashcards;
using Infrastructure.Persistence.Configurations.Quests;
using Infrastructure.Persistence.Configurations.Studysets;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Group = Domain.Entities.Groups.Group;

namespace Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<User>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    public DbSet<StudySet> StudySets => Set<StudySet>();
    public DbSet<RecentStudySet> RecentStudySets => Set<RecentStudySet>();

    public DbSet<Flashcard> Flashcards => Set<Flashcard>();
    public DbSet<CompletedFlashcard> CompletedFlashcards => Set<CompletedFlashcard>();

    public DbSet<Quest> Quests => Set<Quest>();
    public DbSet<UserQuest> UserQuests => Set<UserQuest>();

    public DbSet<UserExperience> UserExperiences => Set<UserExperience>();
    
    public DbSet<Group> Groups => Set<Group>();
    public DbSet<GroupStudySet> GroupStudySets => Set<GroupStudySet>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfiguration(new StudySetConfiguration());
        modelBuilder.ApplyConfiguration(new GroupStudySetConfiguration());
        modelBuilder.ApplyConfiguration(new RecentStudySetConfiguration());
        
        modelBuilder.ApplyConfiguration(new GroupMemberConfiguration());

        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());
        
        modelBuilder.ApplyConfiguration(new UserQuestConfiguration());
        modelBuilder.ApplyConfiguration(new QuestConfiguration());
        
        modelBuilder.ApplyConfiguration(new UserExperienceConfiguration());
        
        modelBuilder.ApplyConfiguration(new CompletedFlashcardConfiguration());
        modelBuilder.ApplyConfiguration(new FlashcardConfiguration());
     
        
        // modelBuilder.ApplyConfiguration(new StudySetRecordConfiguration());
        // modelBuilder.ApplyConfiguration(new StudySetProgressConfiguration());
    }


}