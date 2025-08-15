using System.ComponentModel.DataAnnotations;

namespace projetoRedes.Domain.Entities;

public class Author
{
    public Int64 Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public ICollection<Book> Books { get; set; } = [];

    public Int64 UserId { get; set; }
}
