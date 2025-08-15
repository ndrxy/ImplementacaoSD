using AutoMapper;
using projetoRedes.Communication.Requests;
using projetoRedes.Communication.Responses;
using projetoRedes.Domain.Repositories;
using projetoRedes.Domain.Services;
using projetoRedes.Exceptions.ExceptionsBase;

namespace projetoRedes.Application.UseCases.Author.AddAuthor;

public class AddAuthorUseCase : IAddAuthorUseCase
{
    private readonly IAuthorRepository _repository;
    private readonly ILoggedUser _loggedUser;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public AddAuthorUseCase(IAuthorRepository repository,
        ILoggedUser loggedUser,
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _loggedUser = loggedUser;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseAdded> Execute(RequestAddAuthor req)
    {
        await Validate(req);

        var loggedUser = await _loggedUser.Logged();

        var author = _mapper.Map<Domain.Entities.Author>(req);

        author.UserId = loggedUser.Id;

        await _repository.Add(author);

        await _unitOfWork.Commit();

        return new ResponseAdded
        {
            Name = author.Name,
        };
    }

    public async Task Validate(RequestAddAuthor req) 
    {
        var validator = new AddAuthorValidator();

        var result = validator.Validate(req);

        req.Name = req.Name.ToUpper();

        var authorExists = await _repository.AuthorAlreadyExists(req.Name);
        if (authorExists)
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, "Autor informado já está cadastrado"));

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorOnAdd(errorMessages);
        }
    }
}
