using Application.Data;
using Application.Interfaces;
using Application.Models;
using Dapper;

namespace Application.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public UserRepository(IDbConnectionFactory connectionFactory) => _connectionFactory = connectionFactory;

    public async Task<bool> CreateAsync(User user)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();

        var result = await connection.ExecuteAsync("""
            INSERT INTO Users (Id, Username, PasswordHash, PasswordSalt)
            VALUES (@Id, @Username, @PasswordHash, @PasswordSalt)
        """, user);

        return result > 0;
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();

        return await connection.QueryFirstOrDefaultAsync<User?>("""
            SELECT * FROM Users WHERE Username = @Username
            """, new { Username = username });
    }
}
