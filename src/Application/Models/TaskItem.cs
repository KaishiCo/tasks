namespace Application.Models;

public class TaskItem
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime Date { get; set; }
    public Guid UserId { get; set; }
}
