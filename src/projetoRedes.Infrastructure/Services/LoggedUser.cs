using Microsoft.EntityFrameworkCore;
using projetoRedes.Domain.Entities;
using projetoRedes.Domain.Security;
using projetoRedes.Domain.Services;
using projetoRedes.Infrastructure.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace projetoRedes.Infrastructure.Services;

public class LoggedUser : ILoggedUser
{
    private readonly MyDbContext _dbContext;
    private readonly ITokenProvider _tokenProvider;

    public LoggedUser(MyDbContext dbContext, 
        ITokenProvider tokenProvider)
    {
        _dbContext = dbContext;
        _tokenProvider = tokenProvider;
    }

    public async Task<User> Logged()
    {
        var token = _tokenProvider.Value();

        var tokenHandler = new JwtSecurityTokenHandler();

        var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

        var identifier = jwtSecurityToken.Claims.First(c => c.Type == ClaimTypes.Sid).Value;

        var userIdentifier = Guid.Parse(identifier);

        return await _dbContext.users.AsNoTracking().FirstAsync(user => user.UserIdentifier == userIdentifier);
    }
}
