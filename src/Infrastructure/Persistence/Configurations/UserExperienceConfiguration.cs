using Domain.Entities.UserExperiences;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class UserExperienceConfiguration : IEntityTypeConfiguration<UserExperience>
{
    public void Configure(EntityTypeBuilder<UserExperience> builder)
    {
        builder
            .HasOne(ux => ux.User)
            .WithOne(ux => ux.Experience)
            .HasForeignKey<UserExperience>(e => e.UserId);
    }
}