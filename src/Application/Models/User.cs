namespace Application.Models;

public class User
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Username { get; init; } = null!;
    public byte[] PasswordHash { get; set; } = null!;
    public byte[] PasswordSalt { get; set; } = null!;
}
