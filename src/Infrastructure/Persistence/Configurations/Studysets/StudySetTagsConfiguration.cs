using Domain.Entities.Studysets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Studysets;

public class StudySetTagsConfiguration : IEntityTypeConfiguration<StudySetTags>
{
    public void Configure(EntityTypeBuilder<StudySetTags> builder)
    {
        builder
            .HasKey(st => new { st.StudySetId, st.TagId });
        
        builder
            .HasOne(st => st.StudySet)
            .WithMany(s => s.StudySetTags)
            .HasForeignKey(s => s.StudySetId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder
            .HasOne(st => st.Tag)
            .WithMany(t => t.StudySetTags)
            .HasForeignKey(st => st.TagId)
            .OnDelete(DeleteBehavior.Cascade);

            
    }
}