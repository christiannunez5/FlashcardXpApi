using Domain.Entities.Auth;

namespace Domain.Entities.Quests;

public class UserQuest
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public bool IsCompleted { get; set; }
    public DateOnly CurrentQuestDate { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
    
    // navigations
    public string QuestId { get; set; } = string.Empty;
    public Quest? Quest { get; set; }
    public string UserId { get; set; } = string.Empty;
    public User? User { get; set; }
}