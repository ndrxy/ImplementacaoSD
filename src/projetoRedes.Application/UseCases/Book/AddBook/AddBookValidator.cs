using FluentValidation;
using projetoRedes.Communication.Requests;

namespace projetoRedes.Application.UseCases.Book.AddBook;

public class AddBookValidator : AbstractValidator<RequestAddBook>
{
    public AddBookValidator()
    {
        RuleFor(book => book.Title).NotEmpty().WithMessage("O título do livro não pode ser vazio");
        RuleFor(book => book.AuthorName).NotEmpty().WithMessage("O nome do autor não pode ser vazio. Cadastre-o não esteja registrado ainda");
    }
}
