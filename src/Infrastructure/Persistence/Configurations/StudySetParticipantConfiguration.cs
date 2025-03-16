using FlashcardXpApi.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace FlashcardXpApi.Infrastructure.Persistence.Configurations
{
    public class StudySetParticipantConfiguration : IEntityTypeConfiguration<StudySetParticipant>
    {
        public void Configure(EntityTypeBuilder<StudySetParticipant> builder)
        {
            builder.HasKey(sp => new { sp.StudySetId, sp.UserId });

            builder
                .HasOne(sp => sp.User)
                .WithMany(u => u.StudySetParticipants)
                .HasForeignKey(sp => sp.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(sp => sp.StudySet)
                .WithMany(s => s.StudySetParticipants)
                .HasForeignKey(sp => sp.StudySetId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
