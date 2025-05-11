using Domain.Entities.Flashcards;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Flashcards;

public class FlashcardConfiguration : IEntityTypeConfiguration<Flashcard>
{
    public void Configure(EntityTypeBuilder<Flashcard> builder)
    {
        builder
            .HasOne(f => f.StudySet)
            .WithMany(s => s.Flashcards)
            .HasForeignKey(f => f.StudySetId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder
            .Property(f => f.CreatedAt)
            .HasDefaultValueSql("GETDATE()");
    }
}
