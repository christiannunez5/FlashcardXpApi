using System.Security.Claims;
using Application.Common.Abstraction;
using Application.Common.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services.Hubs;

public class GameRoom
{
    public required string Id { get; set; }
    public List<string> Players { get; set; } = new();

}
    

public class EventHubService : Hub
{
    
    private readonly ILogger<EventHubService> _logger;
    private static Dictionary<string, GameRoom> Rooms = new();
    private readonly IUserContext _userContext;
    private readonly IApplicationDbContext _context;
    
    public EventHubService(ILogger<EventHubService> logger, IUserContext userContext, IApplicationDbContext context)
    {
        _logger = logger;
        _userContext = userContext;
        _context = context;
    }
    
    public override async Task OnConnectedAsync()
    {
        await Clients.Client(Context.ConnectionId).SendAsync("connection",
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
    
    public async Task JoinRoom(string roomId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
        
        string userId = _userContext.UserId();
        if (!Rooms.TryGetValue(roomId, out var room))
        {
            room = new GameRoom { Id = roomId };
            Rooms[roomId] = room;
        }
        
        if (!room.Players.Contains(userId))
        {
            room.Players.Add(userId);
        }
        
        await Clients.Group(roomId).SendAsync("Joined", room.Players);
    }
    
    public async Task LeaveRoom(string roomId)
    {
        string userId = _userContext.UserId();
        if (Rooms.TryGetValue(roomId, out var room))
        {
            if (room.Players.Contains(userId))
            {
                room.Players.Remove(userId);
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId);

                await Clients.Group(roomId).SendAsync("PlayerLeft", userId);
            }
          
        }
    }
  
}