namespace FlashcardXpApi.Domain;

public class UserExperience
{
    public string Id { get; set; } =  Guid.NewGuid().ToString();
    public required string UserId { get; init; }
    public User User { get; set; } = null!;
    public int Xp { get; set; } = 0;
    
}