using BlazorSozluk.Application.Interfaces.Repositories;
using BlazorSozluk.Domain.Models;
using BlazorSozluk.Infrastructure.Persistance.Context;

namespace BlazorSozluk.Infrastructure.Persistance.Repositories
{
    public class EmailConfirmationRepository : GenericRepository<EmailConfirmation>, IEmailConfirmationRepository
    {
        public EmailConfirmationRepository(BlazorSozlukContext dbContext) : base(dbContext)
        {
        }
    }
}
