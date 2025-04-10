using Domain.Entities.Auth;
using Domain.Entities.UserExperiences;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        
        builder
            .HasIndex(u => u.Email)
            .IsUnique();

        builder.Property(u => u.ProfilePicUrl)
            .HasDefaultValue("");
        
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