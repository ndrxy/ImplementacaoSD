using projetoRedes.Communication.Requests;
using projetoRedes.Domain.Repositories;

namespace projetoRedes.Application.UseCases.Book.SearchByTitle;

internal class SearchBookByTitleUseCase : ISearchBookByTitleUseCase
{
    public readonly IBookRepository _repository;

    public SearchBookByTitleUseCase(IBookRepository repository)
    {
        _repository = repository;
    }

    public async Task<IList<RequestAddBook>?> Execute(RequestSearch req)
    {
        var bookList = await _repository.SearchByTitle(req.Name);

        if(bookList == null)    
            return null;

        return [.. bookList.Select(b => new RequestAddBook
        {
            Title = b.Title,
            AuthorName = b.Author.Name,
            Description = b.Description,
        })];
    }
}
