namespace Application.Common.Abstraction;

public interface IEventService
{
    Task SendToAll<T>(string name, T payload);
    Task SendToGroup<T>(string name, T payload, string groupId);
    Task SendToOne<T>(string name, T payload, string userId);
    
    Task AddToGroup(string connectionId,  string groupId);    
    Task RemoveFromGroup(string connectionId,  string groupId);

}