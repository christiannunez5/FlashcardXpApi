using Domain.Entities.Auth;

namespace Domain.Entities.Users;

public class UserFollowing
{
    public required string UserId { get; set; }
    public User User { get; set; } = null!;
    
    public required string FollowingId { get; set; }
    public User Following { get; set; } = null!;
}