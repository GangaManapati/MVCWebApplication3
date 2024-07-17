using Microsoft.EntityFrameworkCore;

namespace MVCWebApplication3.Models
{
    public class ProDbContext:DbContext
    {
        public ProDbContext(DbContextOptions<ProDbContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
