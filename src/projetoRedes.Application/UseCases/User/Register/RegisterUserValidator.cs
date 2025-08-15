using FluentValidation;
using projetoRedes.Communication.Requests;

namespace projetoRedes.Application.UseCases.User.Register;

public class RegisterUserValidator : AbstractValidator<RequestRegisterUser>
{
    public RegisterUserValidator()
    {
        RuleFor(user => user.Name).NotEmpty().WithMessage("Nome não pode ser vazio");
        RuleFor(user => user.Email).NotEmpty().WithMessage("Endereço de email não pode ser vazio");
        RuleFor(user => user.Email).EmailAddress().WithMessage("Digite um endereço de email válido");
        RuleFor(user => user.Password.Length).GreaterThanOrEqualTo(6).WithMessage("Senha deve conter ao menos 6 caracteres");
    }
}
