using projetoRedes.Communication.Requests;

namespace projetoRedes.Application.UseCases.Book.SearchByTitle;

public interface ISearchBookByTitleUseCase
{
    public Task<IList<RequestAddBook>?> Execute(RequestSearch req);
}
