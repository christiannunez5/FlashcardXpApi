namespace Domain.Entities.Subjects;

public class Subject
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    public required string Name { get; set; }
}