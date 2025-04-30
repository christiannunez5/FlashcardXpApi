using Domain.Entities.Users;

namespace Domain.Entities.Studysets;

public class StudySetParticipant
{
    public required string StudySetId { get; set; }
    public StudySet StudySet { get; set; } = null!;
    
    public required string UserId { get; set; }
    public User User { get; set; } = null!;
}