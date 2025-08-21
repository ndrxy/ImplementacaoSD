using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using projetoRedes.Domain.Repositories;
using projetoRedes.Domain.Security;
using projetoRedes.Domain.Services;
using projetoRedes.Infrastructure.Data;
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
        AddDbContext(services, configuration);
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

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration) 
    {
        services.AddDbContext<MyDbContext>(options =>
        {
            if (Environment.GetEnvironmentVariable("USE_DOCKER_DB") == "true")
            {
                var dbServer = configuration["DB_SERVER"];
                var dbDatabase = configuration["DB_NAME"];
                var dbUser = configuration["DB_USER"];
                var dbPassword = configuration["DB_PASSWORD"];
                var dbPort = configuration["DB_PORT"];

                var connectionString = $"Server={dbServer};Port={dbPort};Database={dbDatabase};User Id={dbUser};Password={dbPassword};";

                var serverVersion = ServerVersion.AutoDetect(connectionString);

                options.UseMySql(connectionString, serverVersion, mySqlOptions =>
                {
                });
            }

            else
            {
                var connectionString = configuration.GetConnectionString("Connection");
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            }
        });
    }
}
