using Microsoft.EntityFrameworkCore;

namespace ShoppingPortalApi.Models
{
    public class ProductDbContext:DbContext
    {
        public ProductDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }


    }
}
