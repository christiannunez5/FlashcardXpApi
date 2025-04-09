using Application.Common.Abstraction;
using Domain.Entities.Auth;
using Domain.Entities.Flashcards;
using Domain.Entities.Quests;
using Domain.Entities.Studysets;
using Domain.Entities.UserExperiences;
using Infrastructure.Persistence.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<User>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    public DbSet<StudySet> StudySets => Set<StudySet>();
    public DbSet<RecentStudySet> RecentStudySets => Set<RecentStudySet>();
    public DbSet<Flashcard> Flashcards => Set<Flashcard>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfiguration(new StudySetConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());
        modelBuilder.ApplyConfiguration(new UserQuestConfiguration());
        modelBuilder.ApplyConfiguration(new RecentStudySetConfiguration());
        modelBuilder.ApplyConfiguration(new UserExperienceConfiguration());
    }


}