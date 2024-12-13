using Microsoft.EntityFrameworkCore;
using ProductManager.DAL.Entities;

namespace ProductManager.DAL
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions)
            : base(dbContextOptions)
        {

        }

        public DbSet<Product> Products { get; set; }
    }
}
