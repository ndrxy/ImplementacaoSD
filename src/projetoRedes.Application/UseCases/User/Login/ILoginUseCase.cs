using projetoRedes.Communication.Requests;
using projetoRedes.Communication.Responses;

namespace projetoRedes.Application.UseCases.User.Login;

public interface ILoginUseCase
{
    public Task<ResponseRegisteredUser> Execute(RequestLogin req);
}
