using projetoRedes.Domain.Entities;

namespace projetoRedes.Domain.Repositories;

public interface IAuthorRepository
{
    public Task Add(Author author);

    public Task<bool> AuthorAlreadyExists(string author);

    public Task<long> GetIdByName(string name);

    public Task<IList<Author>?> SearchByName(string name);
}
