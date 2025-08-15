using projetoRedes.Communication.Requests;
using projetoRedes.Communication.Responses;
using projetoRedes.Domain.Repositories;

namespace projetoRedes.Application.UseCases.Author.SearchByName;

public class SearchAuthorByNameUseCase : ISearchAuthorByNameUseCase
{
    private readonly IAuthorRepository _repository;

    public SearchAuthorByNameUseCase(IAuthorRepository repository)
    {
        _repository = repository;
    }

    public async Task<IList<ResponseSearchAuthor>?> Execute(RequestSearch req)
    {
        var authorList = await _repository.SearchByName(req.Name);

        if (authorList == null)
            return null;

        return [.. authorList.Select(a => new ResponseSearchAuthor
        {
            Name = a.Name,
            Description = a.Description,
        })];
    }
}
