using AutoMapper;
using projetoRedes.Application.Services.Cryptography;
using projetoRedes.Application.UseCases.User.Register;
using projetoRedes.Communication.Requests;
using projetoRedes.Communication.Responses;
using projetoRedes.Domain.Repositories;
using projetoRedes.Domain.Security;
using projetoRedes.Exceptions.ExceptionsBase;

namespace projetoRedes.Application.UseCases.User;

public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;
    private readonly PasswordEncrypter _passwordEncryper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAccessTokenGenerator _accessTokenGenerator;

    public RegisterUserUseCase(IUserRepository repository,
        IMapper mapper,
        PasswordEncrypter passwordEncrypter,
        IUnitOfWork unitOfWork,
        IAccessTokenGenerator accessTokenGenerator)
    {
        _repository = repository;
        _mapper = mapper;
        _passwordEncryper = passwordEncrypter;
        _unitOfWork = unitOfWork;
        _accessTokenGenerator = accessTokenGenerator;
    }

    public async Task<ResponseRegisteredUser> Execute(RequestRegisterUser req)
    {
        await Validate(req);

        var user = _mapper.Map<Domain.Entities.User>(req);

        user.Password = _passwordEncryper.Encrypt(req.Password);

        user.UserIdentifier = Guid.NewGuid();

        await _repository.Add(user);

        await _unitOfWork.Commit();

        return new ResponseRegisteredUser
        {
            Name = req.Name,
            Tokens = new ResponseToken
            {
                AccessToken = _accessTokenGenerator.Generate(user.UserIdentifier)
            }
        };
    }

    private async Task Validate(RequestRegisterUser req)
    {
        var validator = new RegisterUserValidator();

        var result = validator.Validate(req);

        var emailExists = await _repository.ExistsActiveUserWithEmail(req.Email);
        if (emailExists || result.IsValid == false)
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, "Já existe um usuário cadastrado com o email informado"));

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
