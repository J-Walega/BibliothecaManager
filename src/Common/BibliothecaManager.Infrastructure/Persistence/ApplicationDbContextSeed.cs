using System;
using System.Linq;
using System.Threading.Tasks;
using BibliothecaManager.Domain.Entities.LibraryEntities;
using BibliothecaManager.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace BibliothecaManager.Infrastructure.Persistence;

public static class ApplicationDbContextSeed
{
    public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        var administratorRole = new IdentityRole("Administrator");
        var employeeRole = new IdentityRole("Employee");
        var userRole = new IdentityRole("User");

        if (roleManager.Roles.All(r => r.Name != administratorRole.Name))
        {
            await roleManager.CreateAsync(administratorRole);
            await roleManager.CreateAsync(employeeRole);
            await roleManager.CreateAsync(userRole);
        }



        var defaultUser = new ApplicationUser { Id= "1", UserName = "Mowgli", Name = "J", Surname = "W", Email = "test@test.com", CityId = 2 };

        if (userManager.Users.All(u => u.UserName != defaultUser.UserName))
        {
            await userManager.CreateAsync(defaultUser, "Testing_123");
            await userManager.AddToRolesAsync(defaultUser, new[] { administratorRole.Name });
        }

        var employeeUser = new ApplicationUser { Id = "3", UserName = "Employee", Name = "Pracownik", Surname = "Nazwisko", Email = "employee@employee.com", CityId = 2};
        if(userManager.Users.All(u => u.UserName != employeeUser.UserName))
        {
            await userManager.CreateAsync(employeeUser, "Employee_123");
            await userManager.AddToRolesAsync(employeeUser, new[] { employeeRole.Name });
        }

        var normalUser = new ApplicationUser { Id = "2", UserName = "Guest", Name = "Guest", Surname = "Guest", Email = "guest@guest.com", CityId = 2 };

        if (userManager.Users.All(u => u.UserName != normalUser.UserName))
        {
            await userManager.CreateAsync(normalUser, "Guest_123");
            await userManager.AddToRolesAsync(normalUser, new[] { userRole.Name });
        }
    }

    public static async Task SeedCityDataAsync(ApplicationDbContext context)
    {
        var city = new City { Id = 1, Name = "Lubin", Region="Dolnyśląsk", PostCode = "59-300" };
        if (!context.Cities.Any())
        {
            context.Cities.Add(city);
            await context.SaveChangesAsync();
        }
    }

    public static async Task SeedPublisherDataAsync(ApplicationDbContext context)
    {
        var publisher = new Publisher { Name = "Nowa Era" };
        if(!context.Publishers.Any())
        {
            context.Publishers.Add(publisher);
            await context.SaveChangesAsync();
        }
    }

    public static async Task SeedGenreDataAsync(ApplicationDbContext context)
    {
        var genre = new Genre { GenereName = "Powieść" };
        if(!context.Genres.Any())
        {
            context.Genres.Add(genre);
            await context.SaveChangesAsync();
        }
    }

    public static async Task SeedAuthorDataAsync(ApplicationDbContext context)
    {
        var author = new Author { Name = "Henryk", Surname = "Sienkiewicz" };
        if(!context.Authors.Any())
        {
            context.Authors.Add(author);
            await context.SaveChangesAsync();
        }
    }

    public static async Task SeedLibraryDataAsync(ApplicationDbContext context)
    {
        var library = new Library { Name = "Pierwsza", Address = "Gdzieś", City = context.Cities.FirstOrDefault() };
        if(!context.Libraries.Any())
        {
            context.Libraries.Add(library);
            await context.SaveChangesAsync();
        }
    }
    public static async Task SeedSampleBooksDataAsync(ApplicationDbContext context)
    {

        if (!context.Books.Any())
        {
            var book = new Book
            {
                Title = "Krzyżacy",
                Authors = new[] { context.Authors.FirstOrDefault() },
                Genres = new[] { context.Genres.FirstOrDefault() },
                Publisher = context.Publishers.FirstOrDefault(),
            };
            context.Books.Add(book) ;
        }

        await context.SaveChangesAsync();
    }
}