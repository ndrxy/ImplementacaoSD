using projetoRedes.Application.Services.Cryptography;
using projetoRedes.Communication.Requests;
using projetoRedes.Communication.Responses;
using projetoRedes.Domain.Repositories;
using projetoRedes.Domain.Security;
using projetoRedes.Exceptions.ExceptionsBase;

namespace projetoRedes.Application.UseCases.User.Login;

public class LoginUseCase : ILoginUseCase
{
    private readonly IUserRepository _repository;
    private readonly PasswordEncrypter _passwordEncrypter;
    private readonly IAccessTokenGenerator _accessTokenGenerator;

    public LoginUseCase(IUserRepository repository,
        PasswordEncrypter passwordEncrypter,
        IAccessTokenGenerator accessTokenGenerator)
    {
        _repository = repository;
        _passwordEncrypter = passwordEncrypter;
        _accessTokenGenerator = accessTokenGenerator;
    }

    public async Task<ResponseRegisteredUser> Execute(RequestLogin req)
    {
        var encryptedPassword = _passwordEncrypter.Encrypt(req.Password);

        var user = await _repository.GetByEmailAndPassword(req.Email, encryptedPassword) ?? throw new InvalidLoginException();

        return new ResponseRegisteredUser
        {
            Name = user.Name,
            Tokens = new ResponseToken
            {
                AccessToken = _accessTokenGenerator.Generate(user.UserIdentifier)
            }
        };
    }
}
