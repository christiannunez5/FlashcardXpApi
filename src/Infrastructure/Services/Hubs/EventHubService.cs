using Application.Common.Abstraction;
using Application.Common.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services.Hubs;

public class EventHubService : Hub<IEventClient>
{
    private readonly ILogger<EventHubService> _logger;
    
    public EventHubService(ILogger<EventHubService> logger)
    {
        _logger = logger;
    }
    public override async Task OnConnectedAsync()
    {
        await Clients.Client(Context.ConnectionId).ConnectionMessage(
            $"Thank you for connecting {Context.ConnectionId}"
        );
        await base.OnConnectedAsync();
    }
    
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var userId = Context.UserIdentifier;
        if (userId != null)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, userId);
        }
        await base.OnDisconnectedAsync(exception);
    }
    
    
    public async Task Send(string message)
    {
        _logger.LogInformation($"Sending message: {message}");
        await Clients.All.ReceiveMessage(message);
    }
    
    public async Task SendToRoom(string roomId, string message)
    {
        _logger.LogInformation($"Sending message: {message} to room {roomId}");
        await Clients.Groups(roomId).ReceiveMessage(message);
    }
    
    public async Task MoveCar(string userId, double progress)
    {
        var eventMessage = new EventMessage<object>
        {
            Type = "MoveCar",
            Payload = new EventPayload<object>()
            {
                Data = progress,
                UserId = userId
            }
        };
        
        _logger.LogInformation($"Moving car");
        await Clients.All.ReceiveMessage(eventMessage);
    }
    
    public async Task JoinRoom(String roomId)
    {
        await Groups.AddToGroupAsync(this.Context.ConnectionId, roomId);
        _logger.LogInformation($"User {this.Context.ConnectionId} has joined the room {roomId}");
    }
    
    /*
    public async Task SendToOne(string userId, EventMessage<object> eventMessage)
    {
        eventMessage.Payload.UserId = _userContext.UserId();
        _logger.LogInformation($"Sending message: {eventMessage.Payload.Data} to user: {userId}");
        await Clients.User(userId).ReceiveMessage(eventMessage);
    }
    */
   
}