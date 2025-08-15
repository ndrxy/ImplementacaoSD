using System.Net;

namespace projetoRedes.Exceptions.ExceptionsBase;

public class ErrorOnAdd : SolutionsExceptions
{
    public IList<string> _errorMessages { get; set; }

    public ErrorOnAdd(IList<string> errorMessages) : base(string.Empty)
    {
        _errorMessages = errorMessages;
    }

    public override IList<string> GetErrorMessages() => _errorMessages;

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
}
