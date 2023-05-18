using Application.Services.Common;

namespace Application.Services;

public interface IAuthService
{
    Task<AuthResult> LoginAsync(string username, string password);
}
