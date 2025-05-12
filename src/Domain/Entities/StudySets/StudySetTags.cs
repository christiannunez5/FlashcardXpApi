using Domain.Entities.Tags;

namespace Domain.Entities.Studysets;

public class StudySetTags
{
    public required string StudySetId { get; set; }

    public StudySet StudySet { get; set; } = null!;
    
    public required string TagId { get; set; }
    public Tag Tag { get; set; } = null!;
}