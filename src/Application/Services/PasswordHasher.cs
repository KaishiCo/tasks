using System.Security.Cryptography;
using System.Text;
using Application.Interfaces;

namespace Application.Services;

public class PasswordHasher : IPasswordHasher
{
    public (byte[] passwordHash, byte[] passwordSalt) HashPassword(string password)
    {
        using var hmac = new HMACSHA512();
        var passwordSalt = hmac.Key;
        var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return (passwordHash, passwordSalt);
    }

    public bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
    {
        using var hmac = new HMACSHA512(storedSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return computedHash.SequenceEqual(storedHash);
    }
}
