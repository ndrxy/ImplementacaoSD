using Microsoft.Extensions.DependencyInjection;
using projetoRedes.Application.Services.AutoMapper;
using projetoRedes.Application.Services.Cryptography;
using projetoRedes.Application.UseCases.Author.AddAuthor;
using projetoRedes.Application.UseCases.Author.SearchByName;
using projetoRedes.Application.UseCases.Book.AddBook;
using projetoRedes.Application.UseCases.Book.SearchByTitle;
using projetoRedes.Application.UseCases.User;
using projetoRedes.Application.UseCases.User.Login;
using projetoRedes.Application.UseCases.User.Register;

namespace projetoRedes.Application;

public static class DependencyInjectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddUseCases(services);
        AddAutoMapper(services);
        AddPasswordEncrypter(services);
    }

    private static void AddUseCases(IServiceCollection services)
    {
        //user
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<ILoginUseCase, LoginUseCase>();

        //author
        services.AddScoped<IAddAuthorUseCase, AddAuthorUseCase>();
        services.AddScoped<ISearchAuthorByNameUseCase, SearchAuthorByNameUseCase>();

        //book
        services.AddScoped<IAddBookUseCase, AddBookUseCase>();
        services.AddScoped<ISearchBookByTitleUseCase, SearchBookByTitleUseCase>();
    }

    private static void AddPasswordEncrypter(IServiceCollection services)
    {
        services.AddScoped(options => new PasswordEncrypter());
    }

    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddScoped(option => new AutoMapper.MapperConfiguration(options =>
        {
            options.AddProfile(new AutoMapping());
        }).CreateMapper());
    }
}
