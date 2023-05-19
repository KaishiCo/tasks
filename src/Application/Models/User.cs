namespace Application.Models;

public class User
{
    public required Guid Id { get; init; } = Guid.NewGuid();
    public required string Username { get; init; }
    public byte[] PasswordHash { get; set; } = null!;
    public byte[] PasswordSalt { get; set; } = null!;
}
