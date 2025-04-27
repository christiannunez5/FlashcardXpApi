using Domain.Entities.Studysets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Persistence.Configurations;

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
            .HasMany(s => s.Flashcards)
            .WithOne(f => f.StudySet)
            .HasForeignKey(f => f.StudySetId)  
            .OnDelete(DeleteBehavior.SetNull);

        builder
            .Property(fs => fs.CreatedAt)
            .HasDefaultValueSql("getDate()");
    
        builder
            .Property(s => s.UpdatedAt)
            .HasDefaultValueSql("getDate()");

        builder
            .Property(s => s.IsPublic)
            .HasDefaultValue(true);
    }
}
