using Application.Common.Abstraction;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.Services;

public class EventHub : Hub<INotificationClient>
{
    public override async Task OnConnectedAsync()
    {
        await Clients.Client(Context.ConnectionId).ReceiveMessage(
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
        await Clients.All.SendMessage(message);
    }
   
}