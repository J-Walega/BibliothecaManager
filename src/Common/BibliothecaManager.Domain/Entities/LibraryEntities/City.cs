using System.Collections.Generic;
using BibliothecaManager.Domain.Common;

namespace BibliothecaManager.Domain.Entities.LibraryEntities
{
    public class City : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Region { get; set; }
        public string PostCode { get; set; }

        public ICollection<Library> Libraries { get; set; }
    }
}
