using Domain.Entities.Studysets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Studysets;


public class StudySetRecordConfiguration : IEntityTypeConfiguration<StudySetRecord>
{
    public void Configure(EntityTypeBuilder<StudySetRecord> builder)
    {
        builder
            .HasKey(sr => new { sr.StudiedById, sr.StudySetId });
        
        builder
            .HasOne(sr => sr.StudiedBy)
            .WithMany(u => u.StudySetRecords)
            .HasForeignKey(sr => sr.StudiedById)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder
            .HasOne(sr => sr.StudySet)
            .WithMany(s => s.StudySetRecords)
            .HasForeignKey(s => s.StudySetId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}



