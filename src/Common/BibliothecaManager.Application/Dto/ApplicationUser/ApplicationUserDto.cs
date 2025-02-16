using System.Collections.Generic;
using BibliothecaManager.Domain.Entities.LibraryEntities;

namespace BibliothecaManager.Application.Dto.ApplicationUser;

public class ApplicationUserDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public IList<string> Roles { get; set; }
    public IList<Borrow> Borrows { get; set; }
}
