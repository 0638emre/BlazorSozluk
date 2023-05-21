using BlazorSozluk.Application.Interfaces.Repositories;
using BlazorSozluk.Domain.Models;
using BlazorSozluk.Infrastructure.Persistance.Context;

namespace BlazorSozluk.Infrastructure.Persistance.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(BlazorSozlukContext dbContext) : base(dbContext)
        {
        }
    }
}