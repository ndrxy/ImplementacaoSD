using Microsoft.EntityFrameworkCore;
using projetoRedes.Domain.Entities;
using projetoRedes.Domain.Repositories;
using projetoRedes.Infrastructure.Data;

namespace projetoRedes.Infrastructure.DataAcces;

public class UserRepository : IUserRepository
{
    private readonly MyDbContext _dbContext;

    public UserRepository(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(User user) => await _dbContext.AddAsync(user);

    public async Task<bool> ExistsActiveUserWithEmail(string email) => await _dbContext.users.AnyAsync(user => user.Email.Equals(email));

    public async Task<bool> ExistsActiveUserWithIdentifier(Guid userIdentifier) => await _dbContext.users.AnyAsync(user => userIdentifier.Equals(userIdentifier));

    public async Task<User?> GetByEmailAndPassword(string email, string password)
    {
        return await _dbContext
            .users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Email.Equals(email) && user.Password.Equals(password));
    }
}
