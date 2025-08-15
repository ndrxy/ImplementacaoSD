using System.Net;

namespace projetoRedes.Exceptions.ExceptionsBase;

public abstract class SolutionsExceptions : SystemException
{
    protected SolutionsExceptions(string message) : base(message) { }

    public abstract IList<string> GetErrorMessages();
    public abstract HttpStatusCode GetStatusCode();
}
