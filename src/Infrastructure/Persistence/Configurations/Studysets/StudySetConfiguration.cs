using Domain.Entities.Studysets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Studysets;

public class StudySetConfiguration : IEntityTypeConfiguration<StudySet>
{
    public void Configure(EntityTypeBuilder<StudySet> builder)
    {
        builder
            .HasOne(s => s.CreatedBy)
            .WithMany(u => u.StudySets)
            .HasForeignKey(s => s.CreatedById)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder
            .Property(s => s.IsPublic)
            .HasDefaultValue(true);
    }
}
