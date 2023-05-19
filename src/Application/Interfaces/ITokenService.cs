namespace Application.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(Guid userId);
}
