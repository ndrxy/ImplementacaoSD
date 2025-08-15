using Microsoft.IdentityModel.Tokens;
using projetoRedes.Domain.Security;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace projetoRedes.Infrastructure.Security.Tokens;

public class JwtTokenGenerator : JwtTokenHandler, IAccessTokenGenerator
{
    private readonly int _expirationTimeMinutes;
    private readonly string _signingKey;

    public JwtTokenGenerator(int expirationTimeMinutes,
        string signingKey)
    {
        _expirationTimeMinutes = expirationTimeMinutes;
        _signingKey = signingKey;
    }

    public string Generate(Guid userIdentifier)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Sid, userIdentifier.ToString())
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_expirationTimeMinutes),
            SigningCredentials = new SigningCredentials(SecurityKey(_signingKey), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(securityToken);
    }
}
