using BibliothecaManager.Domain.Common;

namespace BibliothecaManager.Domain.Entities.LibraryEntities
{
    public class Genre : AuditableEntity
    {
        public int Id { get; set; }
        public string GenereName { get; set; }
    }
}
