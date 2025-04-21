namespace Domain.Entities.Auth;

public class RefreshToken
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Token { get; set; }
    
    public required string UserId { get; init; }
    public User User { get; set; } = null!;

    public DateTime ExpiresOnUtc { get; set; }
}