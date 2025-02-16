using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using BibliothecaManager.Application.Common.Interfaces;
using BibliothecaManager.Domain.Common;
using BibliothecaManager.Domain.Entities.LibraryEntities;
using BibliothecaManager.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BibliothecaManager.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    private readonly IDateTime _dateTime;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDomainEventService _domainEventService;

    public ApplicationDbContext(
        DbContextOptions options,
        ICurrentUserService currentUserService,
        IDateTime dateTime,
        IDomainEventService domainEventService) : base(options)
    {
        _dateTime = dateTime;
        _domainEventService = domainEventService;
        _currentUserService = currentUserService;
    }

    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<BookItem> BookItems { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Library> Libraries { get; set; }
    public DbSet<Publisher> Publishers { get; set; }
    public DbSet<BookItemStatus> BookItemStatuses { get; set; }
    public DbSet<Borrow> Borrows { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (EntityEntry<AuditableEntity> entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.Creator = _currentUserService.UserId;
                    entry.Entity.CreateDate = _dateTime.Now;
                    break;
                case EntityState.Modified:
                    entry.Entity.Modifier = _currentUserService.UserId;
                    entry.Entity.ModifyDate = _dateTime.Now;
                    break;
            }
        }

        foreach (EntityEntry<BookItemStatus> entry in ChangeTracker.Entries<BookItemStatus>())
        {
            switch (entry.State)
            {
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = _dateTime.Now;
                    break;
                case EntityState.Added:
                    var oldEntry = BookItemStatuses.Where(i => i.BookItemId == entry.Entity.BookItemId).OrderBy(d => d.CreateDate).FirstOrDefault();
                    if (oldEntry != null)
                    {
                        BookItemStatuses.Where(i => i.BookItemId == entry.Entity.BookItemId).OrderBy(d => d.CreateDate).FirstOrDefault().Active = false;
                    }                   
                    entry.Entity.Active = true;
                    break;
            }
        }

        foreach (EntityEntry<BookItem> entry in ChangeTracker.Entries<BookItem>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    break;
            }
        }

        var result = await base.SaveChangesAsync(cancellationToken);

        await DispatchEvents();

        return result;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        builder
            .Entity<BookItemStatus>()
            .Property(d => d.Status)
            .HasConversion<string>();

        base.OnModelCreating(builder);
    }

    private async Task DispatchEvents()
    {
        while (true)
        {
            var domainEventEntity = ChangeTracker
                .Entries<IHasDomainEvent>()
                .Select(x => x.Entity.DomainEvents)
                .SelectMany(x => x)
                .FirstOrDefault(domainEvent => !domainEvent.IsPublished);

            if (domainEventEntity == null) break;

            domainEventEntity.IsPublished = true;
            await _domainEventService.Publish(domainEventEntity);
        }
    }
}
