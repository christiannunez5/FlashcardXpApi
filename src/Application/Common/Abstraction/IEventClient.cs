using Application.Common.Models;

namespace Application.Common.Abstraction;



public interface IEventClient
{
    Task ReceiveMessage(EventMessage<object> eventMessage);
    Task ConnectionMessage(string message);
    Task ReceiveMessage(string message);
    
}