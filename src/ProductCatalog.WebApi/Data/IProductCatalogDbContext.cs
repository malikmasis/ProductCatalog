using Microsoft.EntityFrameworkCore;
using ProductCatalog.Entities;
using System.Threading.Tasks;

namespace ProductCatalog.WebApi.Data
{
    public interface IProductCatalogDbContext
    {
        public DbSet<Product> Products { get; set; }
        Task<int> SaveChangesAsync();
    }
}
