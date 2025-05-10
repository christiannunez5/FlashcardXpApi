using Application.Common.Abstraction;
using Infrastructure.Services.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.Services;

public class EventService : IEventService
{
    private readonly IHubContext<EventHubService> _hubContext;

    public EventService(IHubContext<EventHubService> hubContext)
    {
        _hubContext = hubContext;
    }
    
    public Task SendToAll<T>(string name, T payload)
    {
        return _hubContext.Clients.All.SendAsync(name, payload);
    }
    
    public Task SendToGroup<T>(string name, T payload, string groupId)
    {
        return _hubContext.Clients.Groups(groupId).SendAsync(name, payload);
    }

    public Task SendToOne<T>(string name, T payload, string userId)
    {
        return _hubContext.Clients.Client(userId).SendAsync(name, payload);
    }

    public Task AddToGroup(string connectionId, string groupId)
    {
        return _hubContext.Groups.AddToGroupAsync(connectionId, groupId);
    }
    
    public Task RemoveFromGroup(string connectionId, string groupId)
    {
        return _hubContext.Groups.RemoveFromGroupAsync(connectionId, groupId);
    }
}