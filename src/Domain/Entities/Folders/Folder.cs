using Domain.Entities.Studysets;
using Domain.Entities.Users;

namespace Domain.Entities.Folders;

public class Folder
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string Name { get; set; } = string.Empty;
    
    // navigations
    public required string CreatedById { get; init; }
    public User CreatedBy { get; set; } = null!;
    
    public string? ParentFolderId  { get; init; }
    public Folder? ParentFolder { get; set; }
    
    public ICollection<Folder> SubFolders { get; init; } = new List<Folder>();
    
    public ICollection<StudySet> StudySets { get; init; } = new List<StudySet>();
}