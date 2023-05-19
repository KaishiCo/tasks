using Application.Interfaces;
using Application.Models;
using Dapper;

namespace Application.Data;

public class DatabaseInitializer
{
    private readonly IDbConnectionFactory _connectionFactory;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public DatabaseInitializer(IDbConnectionFactory connectionFactory, IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _connectionFactory = connectionFactory;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task InitializeAsync()
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();

        await connection.ExecuteAsync("""
            CREATE TABLE IF NOT EXISTS Users(
                Id UUID PRIMARY KEY,
                Username VARCHAR(30) NOT NULL UNIQUE,
                PasswordHash BYTEA NOT NULL,
                PasswordSalt BYTEA NOT NULL)
        """);

        await SeedUserTableAsync();
    }

    private async Task SeedUserTableAsync()
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();

        if (await connection.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM Users") > 0)
            return;

        var (passwordHash, passwordSalt) = _passwordHasher.HashPassword("ourmom");
        var devUser = new User
        {
            Id = Guid.NewGuid(),
            Username = "dev",
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        };

        await _userRepository.CreateAsync(devUser);
    }
}
