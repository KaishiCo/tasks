using Application.Interfaces;
using Application.Models;
using Bogus;
using Dapper;

namespace Application.Data;

public class DatabaseInitializer
{
    private readonly IDbConnectionFactory _connectionFactory;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITaskItemRepository _taskItemRepository;

    public DatabaseInitializer(IDbConnectionFactory connectionFactory, IUserRepository userRepository, IPasswordHasher passwordHasher, ITaskItemRepository taskItemRepository)
    {
        _connectionFactory = connectionFactory;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _taskItemRepository = taskItemRepository;
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

        await connection.ExecuteAsync("""
            CREATE TABLE IF NOT EXISTS TaskItems(
                Id UUID PRIMARY KEY,
                Name VARCHAR(30) NOT NULL,
                Description VARCHAR(255),
                Date TIMESTAMP NOT NULL,
                IsCompleted BOOLEAN NOT NULL DEFAULT FALSE,
                UserId UUID NOT NULL REFERENCES Users(Id))
        """);

        await SeedDevData();
    }

    private async Task SeedDevData()
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();

        if (await connection.ExecuteScalarAsync<int>("SELECT COUNT(1) FROM Users") > 0)
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
        var tasks = GenerateFakeTaskData(devUser.Id);
        await _taskItemRepository.CreateAsync(tasks);
    }

    private static IEnumerable<TaskItem> GenerateFakeTaskData(Guid userId)
    {
        return new Faker<TaskItem>()
            .RuleFor(t => t.UserId, _ => userId)
            .RuleFor(t => t.Id, _ => Guid.NewGuid())
            .RuleFor(t => t.Name, f => f.Lorem.Word())
            .RuleFor(t => t.Description, f => f.Lorem.Sentence(3, 6))
            .RuleFor(t => t.IsCompleted, _ => false)
            .RuleFor(t => t.Date, f => f.Date.Future())
            .GenerateBetween(4, 10);
    }
}
