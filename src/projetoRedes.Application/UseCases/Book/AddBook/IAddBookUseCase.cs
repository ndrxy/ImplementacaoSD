using projetoRedes.Communication.Requests;
using projetoRedes.Communication.Responses;

namespace projetoRedes.Application.UseCases.Book.AddBook;

public interface IAddBookUseCase
{
    public Task<ResponseAdded> Execute(RequestAddBook req);
}
