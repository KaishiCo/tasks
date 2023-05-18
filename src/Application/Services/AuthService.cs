using Application.Data;
using Application.Services.Common;

namespace Application.Services;
public class AuthService : IAuthService
{
    private const string TOKEN_TYPE = "Bearer";
    private readonly IDbConnectionFactory _connectionFactory;

    public AuthService(IDbConnectionFactory connectionFactory)
        => _connectionFactory = connectionFactory;

    public async Task<AuthResult> LoginAsync(string username, string password)
    {
        await Task.Delay(100);

        if (password != "ourmom")
            return new AuthResult(false);

        return new AuthResult(
            IsSuccess: true,
            TokenType: TOKEN_TYPE,
            AccessToken: "eyJhb3453463634563456534",
            ExpiresIn: 3600);
    }
}
