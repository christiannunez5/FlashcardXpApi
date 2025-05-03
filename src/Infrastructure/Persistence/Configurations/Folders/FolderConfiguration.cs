using Domain.Entities.Folders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Folders;

public class FolderConfiguration : IEntityTypeConfiguration<Folder>
{
    public void Configure(EntityTypeBuilder<Folder> builder)
    {
        
        builder
            .HasMany(f => f.StudySets)
            .WithOne(s => s.Folder)
            .HasForeignKey(f => f.FolderId)
            .OnDelete(DeleteBehavior.NoAction);
                
        builder
            .HasOne(f => f.ParentFolder)
            .WithMany(f => f.SubFolders)
            .HasForeignKey(f => f.ParentFolderId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder
            .HasOne(f => f.CreatedBy)
            .WithMany(u => u.Folders)
            .HasForeignKey(f => f.CreatedById)
            .OnDelete(DeleteBehavior.Cascade);
    }
}