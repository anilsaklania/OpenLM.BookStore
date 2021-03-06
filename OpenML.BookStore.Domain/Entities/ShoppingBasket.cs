using OpenML.BookStore.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenML.BookStore.Domain.Entities
{
    public class ShoppingBasket: AuditableEntity
    {
        public int ShoppingBasketId { get; set; }
        public int CustomerId{ get; set; }
        public Customer Customer { get; set; }
    }
}
