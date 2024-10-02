using LoggingBestPract.Domain.Entities;

namespace LoggingBestPract.Application.Interfaces.Repositories;

public interface IUserRepository : IRepository<Users, int>
{
}