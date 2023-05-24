using Application.Data;
using Application.Interfaces;
using Application.Models;
using Dapper;

namespace Application.Services
{
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
            INSERT INTO TaskItems(Id, Name, Description, Date, IsCompleted, UserId)
            VALUES(@Id, @Name, @Description, @Date, @IsCompleted, @UserId)
        """, taskItems);

            return result > 0;
        }

        public async Task<bool> CreateAsync(TaskItem taskItem)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();

            var result = await connection.ExecuteAsync("""
            INSERT INTO TaskItems(Id, Name, Description, Date, IsCompleted, UserId)
            VALUES(@Id, @Name, @Description, @Date, @IsCompleted, @UserId)
        """, taskItem);

            return result > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();

            var results = await connection.ExecuteAsync("DELETE FROM TaskItems WHERE Id = @Id", new { Id = id });

            return results > 0;
        }

        public async Task<IEnumerable<TaskItem>> GetAllAsync()
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();

            return await connection.QueryAsync<TaskItem>("SELECT * FROM TaskItems");
        }

        public async Task<TaskItem?> GetByIdAsync(Guid id)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();

            return await connection.QuerySingleOrDefaultAsync<TaskItem>("SELECT * FROM TaskItems WHERE Id = @Id", new { Id = id });
        }

        public async Task<bool> UpdateAsync(TaskItem taskItem)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();

            var result = await connection.ExecuteAsync("""
            UPDATE TaskItems
            SET Name = @Name,
            Description = @Description,
            Date = @Date,
            IsCompleted = @IsCompleted
            WHERE Id = @Id
        """, taskItem);

            return result > 0;
        }
    }
}
