namespace projetoRedes.Domain.Security;

public interface IAccessTokenValidator
{
    public Guid ValidateAndGetUserIdentifier(string token);
}
