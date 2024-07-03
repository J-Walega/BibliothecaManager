using System;
using System.Collections.Generic;
using BibliothecaManager.Application.Dto.LibraryDto;
using BibliothecaManager.Domain.Entities.LibraryEntities;

namespace BibliothecaManager.Application.Dto.ApplicationUser;
public class ApplicationUserAllDto
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }

    public IList<string> Roles { get; set; }

    public string Name { get; set; }
    public string Surname { get; set; }
    public string BirthDate { get; set; }
    public CityDto City { get; set; }
    public string StreetAddress { get; set; }
    public int BuildingNumber { get; set; }
    public int HouseNumber { get; set; }
    public DateTime CreatedAt { get; set; }
    public long PESEL { get; set; }
    public IList<BorrowDto> Borrows { get; set; }
}

public class CityDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Region { get; set; }
    public string PostCode { get; set; }
}