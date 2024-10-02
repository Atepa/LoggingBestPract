using LoggingBestPract.Domain.Contexts;
using LoggingBestPract.Application.Interfaces.Repositories;
using LoggingBestPract.Domain.Entities;

namespace LoggingBestPract.Persistence.Repositories.MySQL
{
    public class ProductRepository : BaseRepository<Products, int>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}