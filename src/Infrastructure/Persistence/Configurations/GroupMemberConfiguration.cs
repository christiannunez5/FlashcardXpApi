using Domain.Entities.Groups;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class GroupMemberConfiguration : IEntityTypeConfiguration<GroupMembers>
{
    
    public void Configure(EntityTypeBuilder<GroupMembers> builder)
    {
        builder
            .HasKey(gp => new { gp.UserId, gp.GroupId });
        
        builder
            .HasOne(gp => gp.User)
            .WithMany(u => u.GroupMembers)
            .HasForeignKey(gp => gp.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder
            .HasOne(gp => gp.Group)
            .WithMany(g => g.GroupMembers)
            .HasForeignKey(gp => gp.GroupId)
            .OnDelete(DeleteBehavior.Restrict);
           
    }
}