using System.Collections;
using System.Collections.Generic;
using BibliothecaManager.Domain.Common;

namespace BibliothecaManager.Domain.Entities.LibraryEntities
{
    public class Author : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
