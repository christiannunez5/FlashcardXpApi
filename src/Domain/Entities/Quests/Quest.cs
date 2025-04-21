namespace Domain.Entities.Quests;

public class Quest
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string Title { get; set; }
    public required string Description { get; set; }
    public int XpReward { get; set; }
    
    public string IconUrl { get; set; } = string.Empty;
    
    public int Goal { get; set; }

}