using Domain.Entities.Users;

namespace Domain.Entities.Studysets;

public class StudySetRating
{
    public required string RatedById { get; init; }
    public User RatedBy { get; init; } = null!;
    
    public StudySet StudySet { get; init; } = null!;
    public required string StudySetId { get; init; }
    
    public int Rating { get; set; }
    
    public string ReviewText { get; set; } = string.Empty;
}