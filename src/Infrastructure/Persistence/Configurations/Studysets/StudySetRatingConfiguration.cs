using Domain.Entities.Studysets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Studysets;

public class StudySetRatingConfiguration : IEntityTypeConfiguration<StudySetRating>
{
    public void Configure(EntityTypeBuilder<StudySetRating> builder)
    {
        builder
            .HasKey(sr => new { sr.StudySetId, sr.RatedById });
        
        builder
            .HasOne(sr => sr.RatedBy)
            .WithMany(u => u.StudySetRatings)
            .HasForeignKey(sr => sr.RatedById)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder
            .HasOne(sr => sr.StudySet)
            .WithMany(s => s.StudySetRatings)
            .HasForeignKey(sr => sr.StudySetId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}