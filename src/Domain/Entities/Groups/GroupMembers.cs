using Domain.Entities.Auth;
using Domain.Entities.Users;

namespace Domain.Entities.Groups;

public class GroupMembers
{
    public required string UserId { get; set; }
    public User User { get; set; } = null!;
    
    public required string GroupId { get; set; }
    public Group Group { get; set; } = null!;
    
}