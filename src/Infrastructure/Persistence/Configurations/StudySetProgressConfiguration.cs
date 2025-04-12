

using Domain.Entities.Studysets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class StudySetProgressConfiguration : IEntityTypeConfiguration<StudySetProgress>
{
    public void Configure(EntityTypeBuilder<StudySetProgress> builder)
    {
        builder
            .HasOne(sp => sp.User)
            .WithMany()
            .HasForeignKey(sp => sp.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(sp => sp.StudySet)  
            .WithMany()
            .HasForeignKey(sp => sp.StudySetId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(sp => sp.StudiedFlashcards)
            .WithOne(sf => sf.StudySetProgress)
            .HasForeignKey(sf => sf.StudySetProgressId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}
