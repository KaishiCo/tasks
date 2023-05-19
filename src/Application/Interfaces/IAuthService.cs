using Application.Services.Common;

namespace Application.Interfaces;

public interface IAuthService
{
    Task<AuthResult> LoginAsync(string username, string password);
}
