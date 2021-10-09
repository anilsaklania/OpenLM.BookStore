using OpenML.BookStore.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenML.BookStore.Domain.Entities
{
    public class ShoppingBasket_Book: AuditableEntity
    {
        public int Id { get; set; }
        public int ShoppingBasketId { get; set; }
        public int BookId { get; set; }
        public ShoppingBasket ShoppingBasket {get;set;}
        public Book Book { get; set; }
    }
}
