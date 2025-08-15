namespace projetoRedes.Domain.Security;

public interface IAccessTokenGenerator
{
    public string Generate(Guid userIdentifier);
}
