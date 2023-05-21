namespace Application.Models;

public class TaskItem
{
    public required Guid Id { get; init; } = Guid.NewGuid();
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required DateTime Date { get; set; }
    public required Guid UserId { get; set; }
}
