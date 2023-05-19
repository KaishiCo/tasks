using Application.Data;
using Application.Interfaces;
using Application.Services.Common;

namespace Application.Services;
public class AuthService : IAuthService
{
    private const string TOKEN_TYPE = "Bearer";
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;

    public AuthService(IUserRepository userRepository, IPasswordHasher passwordHasher, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
    }

    public async Task<AuthResult> LoginAsync(string username, string password)
    {
        var user = await _userRepository.GetByUsernameAsync(username);

        if (user is null
            || !_passwordHasher.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
        {
            return new AuthResult(IsSuccess: false);
        }

        var token = _tokenService.GenerateAccessToken(user.Id);

        return new AuthResult(
            IsSuccess: true,
            TokenType: TOKEN_TYPE,
            AccessToken: token,
            ExpiresIn: 3600);
    }
}
