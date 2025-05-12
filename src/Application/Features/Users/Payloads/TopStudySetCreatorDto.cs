namespace Application.Features.Users.Payloads;

public class TopStudySetCreatorDto
{
    public UserDto User { get; set; } = null!;
    public int StudySetsCreatedCount { get; set; }
}