using FluentValidation;
using projetoRedes.Communication.Requests;

namespace projetoRedes.Application.UseCases.Author.AddAuthor;

public class AddAuthorValidator : AbstractValidator<RequestAddAuthor>
{
    public AddAuthorValidator()
    {
        RuleFor(author => author.Name).NotEmpty().WithMessage("Nome do autor não pode ser vazio");
    }
}
