using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using FlashcardXpApi.Domain;
using FlashcardXpApi.Infrastructure.Persistence.Configurations;

namespace FlashcardXpApi.Infrastructure.Persistence
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<StudySet> StudySets { get; set; }

        public DbSet<Flashcard> Flashcards { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<UserQuest> UserQuests { get; set; }
        public DbSet<Quest> Quests { get; set; }

        public DbSet<FlashcardsCompleted> FlashcardsCompleted { get; set; }

        public DbSet<RecentStudySet> RecentStudySets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new StudySetConfiguration());
            modelBuilder.ApplyConfiguration(new StudySetParticipantConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());
            modelBuilder.ApplyConfiguration(new UserQuestConfiguration());
            modelBuilder.ApplyConfiguration(new FlashcardsCompletedConfiguration());
            modelBuilder.ApplyConfiguration(new RecentStudySetConfiguration());
        }
    }
}
