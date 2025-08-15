namespace projetoRedes.Domain.Repositories;

public interface IUnitOfWork
{
    public Task Commit();
}
