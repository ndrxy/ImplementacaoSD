using AutoMapper;
using projetoRedes.Communication.Requests;
using projetoRedes.Communication.Responses;
using projetoRedes.Domain.Repositories;
using projetoRedes.Domain.Services;
using projetoRedes.Exceptions.ExceptionsBase;

namespace projetoRedes.Application.UseCases.Book.AddBook;

public class AddBookUseCase : IAddBookUseCase
{
    private readonly IBookRepository _repository;
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggedUser _loggedUser;

    public AddBookUseCase(IBookRepository repository,
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IAuthorRepository authorRepository,
        ILoggedUser loggedUser)
    {
        _repository = repository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _authorRepository = authorRepository;
        _loggedUser = loggedUser;
    }

    public async Task<ResponseAdded> Execute(RequestAddBook req)
    {
        await Validate(req);

        var loggedUser = await _loggedUser.Logged();

        var book = _mapper.Map<Domain.Entities.Book>(req);

        var authorId = await _authorRepository.GetIdByName(req.AuthorName);
        book.AuthorId = authorId;

        book.UserId = loggedUser.Id;

        await _repository.Add(book);

        await _unitOfWork.Commit();

        return new ResponseAdded
        {
            Name = book.Title,
        };
    }

    public async Task Validate(RequestAddBook req)
    {
        var validator = new AddBookValidator();

        var result = validator.Validate(req);

        req.Title = req.Title.ToUpper();

        req.AuthorName = req.AuthorName.ToUpper();

        var authorExists = await _authorRepository.AuthorAlreadyExists(req.AuthorName);
        if (!authorExists)
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, "Autor informado não está registrado."));

        var bookExists = await _repository.BookAlreadyExists(req.Title);
        if (bookExists)
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, "Um livro de mesmo nome já está cadastrado."));

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorOnAdd(errorMessages);
        }
    }
}
