using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliothecaManager.Domain.Entities.LibraryEntities;

namespace BibliothecaManager.Application.Dto.LibraryDto;
public class BorrowDto
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public BookItemDto BookItem { get; set; }
    public DateTime? ReturnDate { get; set; }
    public bool IsReturned { get; set; }
}
