using FlashcardXpApi.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlashcardXpApi.Infrastructure.Persistence.Configurations
{
    public class FlashcardsCompletedConfiguration : IEntityTypeConfiguration<FlashcardsCompleted>
    {
        public void Configure(EntityTypeBuilder<FlashcardsCompleted> builder)
        {
            builder.HasKey(fc => new { fc.FlashcardId, fc.UserId });

            builder
                .Property(fc => fc.Date)
                .HasDefaultValueSql("CAST(GETDATE() AS DATE)");

            builder
                .HasOne(fc => fc.User)
                .WithMany(u => u.FlashcardsCompleted)
                .HasForeignKey(fc => fc.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(fc => fc.Flashcard)
                .WithMany(f => f.FlashcardsCompleted)
                .HasForeignKey(fc => fc.FlashcardId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
