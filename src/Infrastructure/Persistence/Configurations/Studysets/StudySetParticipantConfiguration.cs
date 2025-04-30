using Domain.Entities.Studysets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Studysets;

public class StudySetParticipantConfiguration : IEntityTypeConfiguration<StudySetParticipant>
{
    public void Configure(EntityTypeBuilder<StudySetParticipant> builder)
    {
        builder
            .HasOne(sp => sp.User)
            .WithMany(u => u.StudySetParticipants)
            .HasForeignKey(sp => sp.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder
            .HasOne(sp => sp.StudySet)
            .WithMany(s => s.StudySetParticipants)
            .HasForeignKey(sp => sp.StudySetId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}