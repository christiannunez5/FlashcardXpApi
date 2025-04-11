using Domain.Entities.Quests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class UserQuestConfiguration : IEntityTypeConfiguration<UserQuest>
{
    public void Configure(EntityTypeBuilder<UserQuest> builder)
    {
        builder
            .HasOne(uq => uq.Quest)
            .WithMany()
            .HasForeignKey(uq => uq.QuestId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(uq => uq.User)
            .WithMany()
            .HasForeignKey(uq => uq.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    
        builder
            .Property(uq => uq.CurrentQuestDate)
            .HasDefaultValueSql("CAST(GETDATE() AS DATE)");
    }
}