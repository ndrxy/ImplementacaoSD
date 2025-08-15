using projetoRedes.Communication.Requests;
using projetoRedes.Communication.Responses;

namespace projetoRedes.Application.UseCases.User.Register;

public interface IRegisterUserUseCase
{
    public Task<ResponseRegisteredUser> Execute(RequestRegisterUser req);
}
