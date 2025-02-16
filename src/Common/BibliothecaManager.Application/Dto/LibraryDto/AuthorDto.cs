using BibliothecaManager.Domain.Entities.LibraryEntities;
using Mapster;

namespace BibliothecaManager.Application.Dto.LibraryDto;
public class AuthorDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
}
