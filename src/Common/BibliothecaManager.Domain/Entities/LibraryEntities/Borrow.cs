using System;
using BibliothecaManager.Domain.Common;

namespace BibliothecaManager.Domain.Entities.LibraryEntities
{
    public class Borrow : AuditableEntity
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int BookItemId { get; set; }
        public BookItem BookItem { get; set; }
        public DateTime BorrowTime { get; set; }
        public DateTime ReturnDate { get; set; }
        public bool IsReturned { get; set; }
        public float Fine { get; set; }

        public Borrow() 
        {
            BorrowTime = DateTime.UtcNow;
            ReturnDate = DateTime.UtcNow.AddMonths(1);
            IsReturned = false;
        }
    }
}
