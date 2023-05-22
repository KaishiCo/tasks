namespace Application.Services.Common;

public record AuthResult(
    bool IsSuccess,
    string UserId = "",
    string TokenType = "",
    string AccessToken = "",
    int ExpiresIn = 0);
