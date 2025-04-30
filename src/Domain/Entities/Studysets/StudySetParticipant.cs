using Domain.Entities.Users;

namespace Domain.Entities.Studysets;

public class StudySetParticipant
{
    public required string Id { get; init; }
    public string? StudySetId { get; init; }
    public StudySet? StudySet { get; init; }
    public required string UserId { get; set; }
    public User User { get; set; } = null!;
}