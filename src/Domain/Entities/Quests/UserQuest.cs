using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Auth;
using Domain.Entities.Users;

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
    
    // not mapped
    [NotMapped]
    public int CompletedFlashcards { get; set; }
}