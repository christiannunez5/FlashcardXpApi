namespace Application.Common.Abstraction;

public interface INotificationClient
{
    Task ReceiveMessage(string message);
    
    Task SendMessage(string message);
}