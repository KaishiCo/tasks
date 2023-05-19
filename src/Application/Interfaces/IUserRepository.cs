using Application.Models;

namespace Application.Interfaces;

public interface IUserRepository
{
    Task<bool> CreateAsync(User user);
    Task<User?> GetByUsernameAsync(string username);
}
