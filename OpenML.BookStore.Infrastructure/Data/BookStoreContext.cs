using Microsoft.EntityFrameworkCore;

namespace OpenML.BookStore.Infrastructure.Data
{
    public class BookStoreContext:DbContext
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> option):base(option)
        {

        }
        public DbSet<Domain.Entities.Author> Authors { get; set; }
        public DbSet<Domain.Entities.Book> Books { get; set; }
        public DbSet<Domain.Entities.Book_Author> Book_Authors { get; set; }
        public DbSet<Domain.Entities.Customer> Customers { get; set; }
        public DbSet<Domain.Entities.Publisher> Publishers { get; set; }
        public DbSet<Domain.Entities.Warehouse> Warehouses { get; set; }
        public DbSet<Domain.Entities.ShoppingBasket_Book> ShoppingBasket_Books { get; set; }
        public DbSet<Domain.Entities.WareHouse_Book> WareHouse_Books { get; set; }
        public DbSet<Domain.Entities.ShoppingBasket> ShoppingBaskets { get; set; }
    }
}
