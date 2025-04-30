using Domain.Entities.UserExperiences;
using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Users;

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
            .HasOne(u => u.Experience)
            .WithOne(ux => ux.User)
            .HasForeignKey<UserExperience>(ux => ux.UserId);
    }
}