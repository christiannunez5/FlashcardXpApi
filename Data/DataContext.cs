using FlashcardXpApi.Flashcards;
using FlashcardXpApi.Users;
using Microsoft.EntityFrameworkCore;
using FlashcardXpApi.FlashcardSets;

namespace FlashcardXpApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<StudySet> StudySets { get; set; }

        public DbSet<Flashcard> Flashcards  { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.StudySets)
                .WithOne(f => f.CreatedBy)
                .HasForeignKey(f => f.CreatedById)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
               .HasIndex(u => u.Email)
               .IsUnique();

            modelBuilder.Entity<StudySet>()
                .HasMany(fs => fs.Flashcards)
                .WithOne(f => f.StudySet)
                .HasForeignKey(f => f.StudySetId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StudySet>()
                .Property(fs => fs.CreatedAt)
                .HasDefaultValueSql("getDate()");
            
            
        }
    }
}
