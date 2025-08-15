using System.Net;

namespace projetoRedes.Exceptions.ExceptionsBase;

public class InvalidLoginException : SolutionsExceptions
{
    public InvalidLoginException() : base("Email e/ou senha incorreto(s)")
    {
    }

    public override IList<string> GetErrorMessages() => [Message];

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
    
}
