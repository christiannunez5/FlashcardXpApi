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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new StudySetConfiguration());
            modelBuilder.ApplyConfiguration(new StudySetParticipantConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());
        }
    }
}
