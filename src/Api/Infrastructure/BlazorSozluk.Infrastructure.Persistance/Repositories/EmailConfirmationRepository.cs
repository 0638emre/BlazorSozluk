using BlazorSozluk.Application.Interfaces.Repositories;
using BlazorSozluk.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorSozluk.Infrastructure.Persistance.Repositories
{
    public class EmailConfirmationRepository : GenericRepository<EmailConfirmation> , IEmailConfirmationRepository
    {
        protected EmailConfirmationRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
