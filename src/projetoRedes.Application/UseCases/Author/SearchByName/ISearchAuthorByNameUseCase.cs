using projetoRedes.Communication.Requests;
using projetoRedes.Communication.Responses;

namespace projetoRedes.Application.UseCases.Author.SearchByName;

public interface ISearchAuthorByNameUseCase
{
    public Task<IList<ResponseSearchAuthor>?> Execute(RequestSearch req);
}
