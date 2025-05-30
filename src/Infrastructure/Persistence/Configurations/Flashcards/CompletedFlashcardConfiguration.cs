using Domain.Entities.Flashcards;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Flashcards;

public class CompletedFlashcardConfiguration : IEntityTypeConfiguration<CompletedFlashcard>
{
    public void Configure(EntityTypeBuilder<CompletedFlashcard> builder)
    {
        builder.HasKey(fc => new { fc.FlashcardId, fc.UserId });
        
        builder
            .Property(fc => fc.Date)
            .HasDefaultValueSql("CAST(GETDATE() AS DATE)");
        
        builder
            .HasOne(fc => fc.User)
            .WithMany(u => u.CompletedFlashcards)
            .HasForeignKey(fc => fc.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder
            .HasOne(fc => fc.Flashcard)
            .WithMany(f => f.FlashcardsCompleted)
            .HasForeignKey(fc => fc.FlashcardId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}