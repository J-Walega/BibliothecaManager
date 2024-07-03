using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliothecaManager.Domain.Enums;

namespace BibliothecaManager.Application.Dto.LibraryDto;
public class BookItemStatusDto
{
    public int Id { get; set; }
    public bool Active { get; set; }
    public BookStatuses Status { get; set; }
}
