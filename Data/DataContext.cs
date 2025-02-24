using FlashcardXpApi.Flashcards;
using Microsoft.EntityFrameworkCore;
using FlashcardXpApi.FlashcardSets;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using FlashcardXpApi.Users;
using Microsoft.AspNetCore.Identity;

namespace FlashcardXpApi.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        
        public DbSet<StudySet> StudySets { get; set; }

        public DbSet<Flashcard> Flashcards  { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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

            modelBuilder.Entity<StudySet>()
                .Property(s => s.IsPublic)
                .HasDefaultValue(true);
                
        }
    }
}
