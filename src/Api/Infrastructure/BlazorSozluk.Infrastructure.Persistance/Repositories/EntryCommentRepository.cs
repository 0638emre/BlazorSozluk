using BlazorSozluk.Application.Interfaces.Repositories;
using BlazorSozluk.Domain.Models;
using BlazorSozluk.Infrastructure.Persistance.Context;

namespace BlazorSozluk.Infrastructure.Persistance.Repositories
{
    public class EntryCommentRepository : GenericRepository<EntryComment>, IEntryCommentRepository
    {
        public EntryCommentRepository(BlazorSozlukContext dbContext) : base(dbContext)
        {
        }
    }
}
