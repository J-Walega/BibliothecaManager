using System;
using BibliothecaManager.Domain.Common;

namespace BibliothecaManager.Domain.Entities.LibraryEntities
{
    public class Comment : AuditableEntity
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public float Score { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public string UserId { get; set; }
        public string FullName { get; set; }
    }
}
