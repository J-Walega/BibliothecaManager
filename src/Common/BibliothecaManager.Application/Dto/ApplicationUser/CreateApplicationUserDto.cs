using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliothecaManager.Application.Dto.ApplicationUser;
public class CreateApplicationUserDto
{
    public string Email { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public CityDto City { get; set; }
    public string Street { get; set; }
    public int BuildingNumber { get; set; }
    public int HouseNumber { get; set; }
    public long PESEL { get; set; }
}
