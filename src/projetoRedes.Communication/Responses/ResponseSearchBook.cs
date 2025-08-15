using projetoRedes.Communication.Requests;

namespace projetoRedes.Communication.Responses;

public class ResponseSearchBook
{
    public IList<RequestAddBook> BooksList { get; set; } = default!;
}
