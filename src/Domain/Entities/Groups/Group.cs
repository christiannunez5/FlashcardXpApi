using Domain.Entities.Auth;
using Domain.Entities.Studysets;
using Domain.Entities.Users;

namespace Domain.Entities.Groups;

public class Group
{
    public string Id { get; init; } = Guid.NewGuid().ToString();
    
    public string Name { get; init; } = String.Empty;
    
    // navigations
    public required string CreatedById { get; init; }
    public User CreatedBy { get; init; } = null!;
    public List<GroupMembers> GroupMembers { get; init; } = new();
    public ICollection<GroupStudySet> GroupStudySets { get; set; } = new List<GroupStudySet>();
    
    public void AddMember(string userId)
    {
        var newMember = new GroupMembers
        {
            UserId = userId,
            GroupId = Id
        };
        
        GroupMembers.Add(newMember);
    }
    
    
}