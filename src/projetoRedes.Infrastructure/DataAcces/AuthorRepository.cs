using Microsoft.EntityFrameworkCore;
using projetoRedes.Domain.Entities;
using projetoRedes.Domain.Repositories;
using projetoRedes.Infrastructure.Data;

namespace projetoRedes.Infrastructure.DataAcces;

public class AuthorRepository : IAuthorRepository
{
    private readonly MyDbContext _dbContext;

    public AuthorRepository(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Author author) => await _dbContext.authors.AddAsync(author);

    public async Task<bool> AuthorAlreadyExists(string name) => await _dbContext.authors.AnyAsync(author => author.Name.Equals(name));

    public async Task<long> GetIdByName(string name) 
    {
        name = name.ToUpper();

        return await _dbContext.authors
        .Where(a => a.Name == name)
        .Select(a => a.Id)
        .FirstOrDefaultAsync();
    }

    public async Task<IList<Author>?> SearchByName(string name)
    {
        return await _dbContext.authors
        .Where(a => EF.Functions.Like(a.Name.ToLower(), $"%{name}%"))
        .OrderBy(a => a.Name)
        .Take(5)
        .ToListAsync();
    }
}
