namespace Application.Common.Models;


public class EventPayload<T>
{
    public required string UserId { get; set; }
    public required T Data { get; set; }
}

public class EventMessage<T>
{
    public string Type { get; set; } = string.Empty;
    public EventPayload<T> Payload { get; set; }
}
