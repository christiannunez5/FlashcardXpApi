using Domain.Entities.Studysets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class RecentStudySetConfiguration : IEntityTypeConfiguration<RecentStudySet>
{
    public void Configure(EntityTypeBuilder<RecentStudySet> builder)
    {
        builder.HasKey(rs => new { rs.UserId, rs.StudySetId });

        builder
            .Property(rs => rs.AccessedAt)
            .HasDefaultValueSql("GETDATE()");
                
        builder
            .HasOne(rs => rs.User)
            .WithMany(u => u.RecentStudySets)
            .HasForeignKey(rs => rs.UserId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder
            .HasOne(rs => rs.StudySet)
            .WithMany(s => s.RecentStudySets)
            .HasForeignKey(rs => rs.StudySetId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}