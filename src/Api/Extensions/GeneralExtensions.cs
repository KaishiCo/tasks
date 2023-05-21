using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Api.Extensions;

public static class GeneralExtensions
{
    public static string GetUserId(this HttpContext httpContext)
        => httpContext.User.Claims.Single(x => x.Type == "userid").Value;
}
