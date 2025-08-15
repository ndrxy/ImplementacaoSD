using projetoRedes.Domain.Repositories;
using projetoRedes.Infrastructure.Data;

namespace projetoRedes.Infrastructure.DataAcces;

public class UnitOfWork : IUnitOfWork
{
    private readonly MyDbContext _dbContext;

    public UnitOfWork(MyDbContext dbContext) => _dbContext = dbContext;

    public async Task Commit() => await _dbContext.SaveChangesAsync();
}
