using LoggingBestPract.Domain.Contexts;
using LoggingBestPract.Application.Interfaces.Repositories;
using LoggingBestPract.Domain.Entities;

namespace LoggingBestPract.Persistence.Repositories.MySQL
{
    public class UserRepository : BaseRepository<Users, int>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}