using Application.Models;

namespace Application.Interfaces;

public interface ITaskItemRepository
{
    Task<IEnumerable<TaskItem>> GetAllAsync();
    Task<bool> CreateAsync(IEnumerable<TaskItem> taskItems);
    Task<bool> CreateAsync(TaskItem taskItem);
    Task<bool> UpdateAsync(TaskItem taskItem);
    Task<bool> DeleteAsync(Guid id);
    Task<TaskItem?> GetByIdAsync(Guid id);
}
