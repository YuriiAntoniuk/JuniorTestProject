using Microsoft.EntityFrameworkCore;

namespace JuniorTestProject.Models
{
    public class BookContext : DbContext
    {
        public BookContext() { }
        public BookContext(DbContextOptions<BookContext> context)
            :base(context)
        {
            Database.EnsureCreated();
        }

        public DbSet<Book> Books { get; set; }
    }
}
