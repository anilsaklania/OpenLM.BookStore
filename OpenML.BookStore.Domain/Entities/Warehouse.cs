using OpenML.BookStore.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenML.BookStore.Domain.Entities
{
    public class Warehouse: AuditableEntity
    {
        public int WareHouseId { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
