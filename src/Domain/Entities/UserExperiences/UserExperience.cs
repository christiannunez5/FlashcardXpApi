using Domain.Entities.Auth;
using Domain.Entities.Users;

namespace Domain.Entities.UserExperiences;

public class UserExperience
{
    public string Id { get; set; } =  Guid.NewGuid().ToString();
    public required string UserId { get; init; }
    public User User { get; set; } = null!;
    public int Xp { get; set; } = 0;
    
    public Level GetLevel =>
        Xp switch
        {
            < 250 => Level.Herald,
            < 500 => Level.Guardian,
            < 1000 => Level.Crusader,
            < 2000 => Level.Archon,
            < 3500 => Level.Legend,
            < 5000 => Level.Ancient,
            < 10000 => Level.Divine,
            _ => Level.Immortal
        };
    
    public int GetMaxXp =>
        GetLevel switch
        {
            Level.Herald => 250,
            Level.Guardian => 500,
            Level.Crusader => 1000,
            Level.Archon => 2000,
            Level.Legend => 3500,
            Level.Ancient => 5000,
            Level.Divine => 10000,
            Level.Immortal => 15000,
            _ => 0
        };
}