using Application.Models;

namespace Application.Interfaces;

public interface ITaskItemRepository
{
    Task<IEnumerable<TaskItem>> GetAllAsync();
    Task<bool> CreateAsync(IEnumerable<TaskItem> taskItems);
    Task<bool> CreateAsync(TaskItem taskItem);
    // Task<TaskItem> UpdateAsync(TaskItem taskItem);
    // Task Deletesync(Guid id);
}
