using Microsoft.EntityFrameworkCore;
using LoggingBestPract.Domain.Entities;

namespace LoggingBestPract.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Brands> Brands { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Products> Products { get; set; }
    }
}
