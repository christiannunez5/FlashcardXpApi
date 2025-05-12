using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Studysets;

namespace Domain.Entities.Tags;

[Table("Tag")]
public class Tag
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    public required string Name { get; set; }
    
    public string ImageUrl { get; set; } = string.Empty;
    
    public ICollection<StudySetTags> StudySetTags { get; set; } = new List<StudySetTags>();
    
}