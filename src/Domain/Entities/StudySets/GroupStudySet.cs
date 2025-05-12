using Domain.Entities.Groups;

namespace Domain.Entities.Studysets;

public class GroupStudySet
{
    public required string GroupId { get; set; }
    public Group Group { get; set; } = null!;
    public string? StudySetId { get; set; }
    public StudySet? StudySet { get; set; }
}