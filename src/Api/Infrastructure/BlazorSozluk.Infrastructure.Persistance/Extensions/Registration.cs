using BlazorSozluk.Application.Interfaces.Repositories;
using BlazorSozluk.Infrastructure.Persistance.Context;
using BlazorSozluk.Infrastructure.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorSozluk.Infrastructure.Persistance.Extensions
{
    public static class Registration
    {
        public static IServiceCollection AddInfrastructureRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BlazorSozlukContext>(conf =>
            {
                var connStr = configuration["BlazorSozlukDB"];
                conf.UseSqlServer(connStr, opt =>
                {
                    opt.EnableRetryOnFailure(); //veritabanına bağlanırken hata alırsan yeniden dene.
                });
            });

            //var seedData = new SeedData();
            //seedData.SeedAsync(configuration).GetAwaiter().GetResult();//seed dataların yüklenmesi ve sonuçlanmasını bekliyoruz.

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IEmailConfirmationRepository, EmailConfirmationRepository>();
            services.AddScoped<IEntryRepository, EntryRepository>();
            services.AddScoped<IEntryCommentRepository, EntryCommentRepository>();


            return services;
        }
    }
}