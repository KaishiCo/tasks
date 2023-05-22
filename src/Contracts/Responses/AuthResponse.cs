namespace Contracts.Responses;

public record AuthResponse(
    string TokenType,
    string AccessToken,
    int ExpiresIn,
    string UserId);
