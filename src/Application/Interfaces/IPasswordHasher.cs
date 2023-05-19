namespace Application.Interfaces;

public interface IPasswordHasher
{
    (byte[] passwordHash, byte[] passwordSalt) HashPassword(string password);
    bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt);
}
