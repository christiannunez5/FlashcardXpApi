using FlashcardXpApi.Flashcards;
using Microsoft.EntityFrameworkCore;
using FlashcardXpApi.FlashcardSets;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using FlashcardXpApi.Users;
using Microsoft.AspNetCore.Identity;
using FlashcardXpApi.StudySets;

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
               .HasIndex(u => u.Email)
               .IsUnique();

            modelBuilder.Entity<User>()
                .HasMany(u => u.StudySets)
                .WithOne(s => s.CreatedBy)
                .HasForeignKey(u => u.CreatedById);

            modelBuilder.Entity<StudySet>()
                .HasOne(s => s.CreatedBy)
                .WithMany(u => u.StudySets)
                .HasForeignKey(s => s.CreatedById)
                .OnDelete(DeleteBehavior.SetNull);

 
            modelBuilder.Entity<StudySet>()
                .Property(s => s.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<StudySet>()
                .Property(fs => fs.CreatedAt)
                .HasDefaultValueSql("getDate()");

            modelBuilder.Entity<StudySet>()
                .Property(s => s.IsPublic)
                .HasDefaultValue(true);

            modelBuilder.Entity<StudySetParticipant>()
                .HasKey(sp => new { sp.StudySetId, sp.UserId });

            modelBuilder.Entity<StudySetParticipant>()
                .HasOne(sp => sp.User)
                .WithMany(u => u.StudySetParticipants)
                .HasForeignKey(sp => sp.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StudySetParticipant>()
                .HasOne(sp => sp.StudySet)
                .WithMany(s => s.StudySetParticipants)
                .HasForeignKey(sp => sp.StudySetId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
