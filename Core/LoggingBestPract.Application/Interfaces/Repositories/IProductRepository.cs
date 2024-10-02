using LoggingBestPract.Domain.Entities;

namespace LoggingBestPract.Application.Interfaces.Repositories;

public interface IProductRepository : IRepository<Products, int>
{
}