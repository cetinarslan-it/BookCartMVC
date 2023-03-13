using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.DataAccess
{
    public class LibraryDBContext : DbContext
    {
        public LibraryDBContext(DbContextOptions<LibraryDBContext> options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set;}
    }
}
