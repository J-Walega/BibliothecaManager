using System.Collections.Generic;
using BibliothecaManager.Domain.Entities.LibraryEntities;

namespace BibliothecaManager.Application.Dto.LibraryDto;
public class BookItemDto
{
    public int Id { get; set; }
    public BookDto Book { get; set; }
    public IList<BookItemStatusDto> Status { get; set; }
}
