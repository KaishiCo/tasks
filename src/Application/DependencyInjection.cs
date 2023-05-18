using Application.Data;
using Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration config)
    {
        services.AddSingleton<IDbConnectionFactory>(_ => new PostgresConnectionFactory(
            config["Database:ConnectionString"]!
        ));
        services.AddSingleton<IAuthService, AuthService>();
        return services;
    }
}
