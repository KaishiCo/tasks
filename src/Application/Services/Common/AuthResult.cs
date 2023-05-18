namespace Application.Services.Common;

public record AuthResult(
    bool IsSuccess,
    string TokenType = "",
    string AccessToken = "null",
    int ExpiresIn = 0);
