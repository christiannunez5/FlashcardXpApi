using Domain.Entities.Studysets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Studysets;

public class GroupStudySetConfiguration : IEntityTypeConfiguration<GroupStudySet>
{
    public void Configure(EntityTypeBuilder<GroupStudySet> builder)
    {
        builder
            .HasKey(gs => new { gs.StudySetId, gs.GroupId });
        
        builder
            .HasOne(gs => gs.Group)
            .WithMany(g => g.GroupStudySets)
            .HasForeignKey(gs => gs.GroupId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder
            .HasOne(gs => gs.StudySet)
            .WithMany(s => s.GroupStudySets)
            .HasForeignKey(gs => gs.StudySetId)
            .OnDelete(DeleteBehavior.ClientCascade);
        
    }
}