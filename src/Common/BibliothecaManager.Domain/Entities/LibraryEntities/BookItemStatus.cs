using System;
using System.Collections.Generic;
using System.Text;
using BibliothecaManager.Domain.Common;
using BibliothecaManager.Domain.Enums;

namespace BibliothecaManager.Domain.Entities.LibraryEntities
{
    public class BookItemStatus : AuditableEntity
    {
        public int Id { get; set; }
        public int BookItemId { get; set; }
        public BookItem BookItem { get; set; }
        public BookStatuses Status { get; set; }
        public bool Active { get; set; }
        public DateTime UpdatedAt { get; set; }

        public BookItemStatus()
        {        
            Active = true;
        }
    }
}
