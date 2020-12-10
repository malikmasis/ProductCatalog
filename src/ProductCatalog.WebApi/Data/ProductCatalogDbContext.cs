using Microsoft.EntityFrameworkCore;
using ProductCatalog.Entities;
using System.Threading.Tasks;

namespace ProductCatalog.WebApi.Data
{
    public class ProductCatalogDbContext : DbContext, IProductCatalogDbContext
    {

        public ProductCatalogDbContext(DbContextOptions<ProductCatalogDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasIndex(u => u.Code)
                .IsUnique();
        }
    }
}
