using System.Collections.Generic;
using BibliothecaManager.Domain.Common;

namespace BibliothecaManager.Domain.Entities.LibraryEntities
{
    public class Book : AuditableEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public ICollection<Genre> Genres { get; set; }
        public ICollection<Author> Authors { get; set; }
        public ICollection<BookItem> BookItems { get; set; }
        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
