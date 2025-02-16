using System.Collections.Generic;
using BibliothecaManager.Domain.Common;

namespace BibliothecaManager.Domain.Entities.LibraryEntities
{
    public class BookItem : AuditableEntity
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int LibraryId { get; set; }
        public Library Library { get; set; }
        public ICollection<BookItemStatus> BookItemStatus { get; set; }
    }
}
