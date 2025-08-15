using projetoRedes.Communication.Requests;
using projetoRedes.Communication.Responses;

namespace projetoRedes.Application.UseCases.Author.AddAuthor;

public interface IAddAuthorUseCase
{
    public Task<ResponseAdded> Execute(RequestAddAuthor req);
}
