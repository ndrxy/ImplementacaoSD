using Microsoft.EntityFrameworkCore;
using projetoRedes.Domain.Entities;
using projetoRedes.Domain.Repositories;
using projetoRedes.Infrastructure.Data;

namespace projetoRedes.Infrastructure.DataAcces;

public class BookRepository : IBookRepository
{
    private readonly MyDbContext _dbContext;

    public BookRepository(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Book book) => await _dbContext.books.AddAsync(book);

    public async Task<bool> BookAlreadyExists(string title) => await _dbContext.books.AnyAsync(book => book.Title.Equals(title));

    public async Task<IList<Book>?> SearchByTitle(string title)
    {
        title = title.Trim().ToUpper();

        return await _dbContext.books
                    .Include(b => b.Author)
                    .Where(b => EF.Functions.Like(b.Title.ToUpper(), $"%{title}%"))
                    .OrderBy(b => b.Title)
                    .Take(5)
                    .ToListAsync();

    }
}
