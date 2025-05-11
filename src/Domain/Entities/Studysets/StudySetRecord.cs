
using Domain.Entities.Users;
namespace Domain.Entities.Studysets;

public class StudySetRecord
{
    public required string StudySetId  { get; init; }
    public StudySet StudySet { get; set; } = null!;
    
    public required string StudiedById { get; set; }
    public User StudiedBy { get; set; } = null!;
}


