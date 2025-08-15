using projetoRedes.Domain.Entities;

namespace projetoRedes.Domain.Repositories;

public interface IBookRepository
{
    public Task Add(Book book);

    public Task<bool> BookAlreadyExists(string title);

    public Task<IList<Book>?> SearchByTitle(string title);
}
