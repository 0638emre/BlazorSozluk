using BlazorSozluk.Application.Interfaces.Repositories;
using BlazorSozluk.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorSozluk.Infrastructure.Persistance.Repositories
{
    public class EntryCommentRepository : GenericRepository<EntryComment>, IEntryCommentRepository
    {
        protected EntryCommentRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
