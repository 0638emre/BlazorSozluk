using BlazorSozluk.Application.Interfaces.Repositories;
using BlazorSozluk.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorSozluk.Infrastructure.Persistance.Repositories
{
    public class EntryRepository : GenericRepository<Entry>, IEntryRepository
    {
        protected EntryRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
