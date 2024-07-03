using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BibliothecaManager.Domain.Entities.LibraryEntities;
using Microsoft.AspNetCore.Identity;

namespace BibliothecaManager.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string BirthDate { get; set; }
    public int CityId { get; set; }
    public City City { get; set; }
    public string Street { get; set; }
    public int BuildingNumber { get; set; }
    public int HouseNumber { get; set; }
    public DateTime CreatedAt { get; set; }
    [MaxLength(11)]
    [Index(IsUnique = true)]
    public long PESEL { get; set; }
    public ICollection<Borrow> Borrows { get; set;}
    public ICollection<Comment> Comments { get; set; }

    public ApplicationUser()
    {
        CreatedAt = DateTime.UtcNow;
    }
}
