using System.Threading;
using System.Threading.Tasks;
using BibliothecaManager.Domain.Entities;
using BibliothecaManager.Domain.Entities.LibraryEntities;
using Microsoft.EntityFrameworkCore;

namespace BibliothecaManager.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<BookItem> BookItems { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Library> Libraries { get; set; }
    public DbSet<Publisher> Publishers { get; set; }
    public DbSet<Borrow> Borrows { get; set; }
    public DbSet<BookItemStatus> BookItemStatuses { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
