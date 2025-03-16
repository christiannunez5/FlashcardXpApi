using FlashcardXpApi.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace FlashcardXpApi.Infrastructure.Persistence.Configurations
{
    public class StudySetConfiguration : IEntityTypeConfiguration<StudySet>
    {
        public void Configure(EntityTypeBuilder<StudySet> builder)
        {
            builder
                .HasOne(s => s.CreatedBy)
                .WithMany(u => u.StudySets)
                .HasForeignKey(s => s.CreatedById)
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
}
