using Microsoft.EntityFrameworkCore;
using TaskFour_FavouriteBooks_.Models;

namespace TaskFour_FavouriteBooks_.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<BookModel> Books { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
