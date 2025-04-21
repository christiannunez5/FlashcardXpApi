using Domain.Entities.Auth;

namespace Domain.Entities.Studysets;

public class RecentStudySet
{

    public required string StudySetId { get; set; }
    public StudySet StudySet { get; set; } = null!;
    public required string UserId { get; set; }
    public User User { get; set; } = null!;
    
    public DateTime AccessedAt { get; set; } = DateTime.Now;
}