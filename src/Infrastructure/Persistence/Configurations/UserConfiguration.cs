using FlashcardXpApi.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace FlashcardXpApi.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasIndex(u => u.Email)
                .IsUnique();
            
            builder
                .HasMany(u => u.StudySets)
                .WithOne(s => s.CreatedBy)
                .HasForeignKey(u => u.CreatedById);
            
            builder
                .HasOne(u => u.Experience)
                .WithOne(ux => ux.User)
                .HasForeignKey<UserExperience>(ux => ux.UserId);
            
        }
    }
}
