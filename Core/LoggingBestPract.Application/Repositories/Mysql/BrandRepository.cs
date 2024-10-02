using LoggingBestPract.Domain.Contexts;
using LoggingBestPract.Application.Interfaces.Repositories;
using LoggingBestPract.Domain.Entities;

namespace LoggingBestPract.Persistence.Repositories.MySQL
{
    public class BrandRepository : BaseRepository<Brands, int>, IBrandRepository
    {
        public BrandRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}