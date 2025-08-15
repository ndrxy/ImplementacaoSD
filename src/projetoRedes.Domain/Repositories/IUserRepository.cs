using projetoRedes.Domain.Entities;

namespace projetoRedes.Domain.Repositories;

public interface IUserRepository
{
    public Task Add(User user);

    public Task<bool> ExistsActiveUserWithEmail(string email);

    public Task<User?> GetByEmailAndPassword(string email, string password);

    public Task<bool> ExistsActiveUserWithIdentifier(Guid userIdentifier);
}
