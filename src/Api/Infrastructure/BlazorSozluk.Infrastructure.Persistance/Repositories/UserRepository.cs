using BlazorSozluk.Application.Interfaces.Repositories;
using BlazorSozluk.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorSozluk.Infrastructure.Persistance.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        protected UserRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
