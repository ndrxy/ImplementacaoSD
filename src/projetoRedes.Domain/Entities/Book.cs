using System.ComponentModel.DataAnnotations;

namespace projetoRedes.Domain.Entities;

public class Book
{
    public Int64 Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public Author Author { get; set; } = default!;

    public Int64 AuthorId { get; set; }

    public Int64 UserId { get; set; }
}
