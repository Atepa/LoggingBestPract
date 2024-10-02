using Microsoft.EntityFrameworkCore;
using LoggingBestPract.Domain.Entities;

namespace LoggingBestPract.Domain.Contexts;

public class ApplicationDbContext : DbContext
{
    public DbSet<Brands> Brand{ get; set; }
    public DbSet<Products> Product { get; set; }
    public DbSet<Users> User { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
}