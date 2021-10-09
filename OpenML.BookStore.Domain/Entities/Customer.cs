using OpenML.BookStore.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenML.BookStore.Domain.Entities
{
   public class Customer: AuditableEntity
    {
        public int CustomerId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
