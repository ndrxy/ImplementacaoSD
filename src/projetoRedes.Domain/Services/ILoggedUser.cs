using projetoRedes.Domain.Entities;

namespace projetoRedes.Domain.Services;

public interface ILoggedUser
{
    public Task<User> Logged();
}
