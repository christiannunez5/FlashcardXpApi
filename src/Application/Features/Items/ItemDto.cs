namespace Application.Features.Items;


public enum ItemType
{
    Folder,
    StudySets
}

public class ItemDto<TValue>
{
    public ItemType Type { get; set; }
    public TValue Data { get; set; }
}