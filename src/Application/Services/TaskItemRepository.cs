using Application.Data;
using Application.Interfaces;
using Application.Models;
using Dapper;

namespace Application.Services;

public class TaskItemRepository : ITaskItemRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public TaskItemRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<bool> CreateAsync(IEnumerable<TaskItem> taskItems)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();

        var result = await connection.ExecuteAsync("""
            INSERT INTO TaskItems(Id, Name, Description, Date, UserId)
            VALUES(@Id, @Name, @Description, @Date, @UserId)
        """, taskItems);

        return result > 0;
    }

    public async Task<bool> CreateAsync(TaskItem taskItem)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();

        var result = await connection.ExecuteAsync("""
            INSERT INTO TaskItems(Id, Name, Description, Date, UserId)
            VALUES(@Id, @Name, @Description, @Date, @UserId)
        """, taskItem);

        return result > 0;
    }

    public async Task<IEnumerable<TaskItem>> GetAllAsync()
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();

        return await connection.QueryAsync<TaskItem>("SELECT * FROM TaskItems");
    }
}
