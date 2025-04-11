using Domain.Entities.Auth;

namespace Domain.Entities.Quests;

public class UserQuest
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public bool IsCompleted { get; set; }
    public DateOnly CurrentQuestDate { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
    
    // navigations
    public required string QuestId { get; set; }
    public Quest Quest { get; set; } = null!;
    public required string UserId { get; set; }
    public User User { get; set; } = null!;
}