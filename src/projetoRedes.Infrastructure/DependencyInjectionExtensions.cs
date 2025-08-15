using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using projetoRedes.Domain.Repositories;
using projetoRedes.Domain.Security;
using projetoRedes.Domain.Services;
using projetoRedes.Infrastructure.DataAcces;
using projetoRedes.Infrastructure.Security.Tokens;
using projetoRedes.Infrastructure.Services;

namespace projetoRedes.Infrastructure;

public static class DependencyInjectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddRepositories(services);
        AddTokens(services);
        AddLoggedUser(services);
    }

    public static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<IBookRepository, BookRepository>();
    }

    private static void AddTokens(IServiceCollection services)
    {
        var expirationTimeMinutes = 60;
        var signingKey = "8UlH`6{5sK2n#~;4+f<v4:$~P4YL,,k8";

        services.AddScoped<IAccessTokenGenerator>(option => new JwtTokenGenerator(expirationTimeMinutes, signingKey!));
        services.AddScoped<IAccessTokenValidator>(option => new JwtTokenValidator(signingKey!));
    }

    private static void AddLoggedUser(IServiceCollection services) => services.AddScoped<ILoggedUser, LoggedUser>();
}
