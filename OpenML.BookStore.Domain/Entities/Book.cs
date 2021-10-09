using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace OpenML.BookStore.Domain.Entities
{
    public class Book
    {
        public int BookId { get; set; }
        public string ISBN { get; set; }
        public string PublisherName { get; set; }
        public int Year { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public int Pub_Id { get; set; }
        public Publisher Publisher { get; set; }
    }
}
