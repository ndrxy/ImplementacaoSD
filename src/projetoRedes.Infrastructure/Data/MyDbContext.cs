using Microsoft.EntityFrameworkCore;
using projetoRedes.Domain.Entities;

namespace projetoRedes.Infrastructure.Data;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Domain.Entities.User> users { get; set; }
    public DbSet<Author> authors { get; set; }
    public DbSet<Book> books { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyDbContext).Assembly);
    }
}
