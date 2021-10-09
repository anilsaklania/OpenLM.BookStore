using OpenML.BookStore.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenML.BookStore.Domain.Entities
{
    public class WareHouse_Book: AuditableEntity
    {
        public int Id { get; set; }
        public int WareHouseCodeId { get; set; }
        public int BookId { get; set; }
        public int Count { get; set; }
        public Warehouse Warehouse { get; set; }
        public Book Book { get; set; }
    }
}
