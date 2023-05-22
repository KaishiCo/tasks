using Application.Interfaces;
using Contracts.Requests;
using Contracts.Responses;

namespace Api.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/auth/login", async (LoginRequest request, IAuthService authService) =>
        {
            var authResult = await authService.LoginAsync(request.Username, request.Password);
            if (!authResult.IsSuccess)
                return Results.Unauthorized();

            return Results.Ok(new AuthResponse(
                authResult.TokenType,
                authResult.AccessToken,
                authResult.ExpiresIn,
                authResult.UserId));
        });
    }
}
