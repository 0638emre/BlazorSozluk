using BlazorSozluk.Common.Infrastructure;
using BlazorSozluk.Domain.Models;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;

namespace BlazorSozluk.Infrastructure.Persistance.Context
{
    internal class SeedData
    {
        public static List<User> GetUsers()
        {
            var result = new Faker<User>("tr")
                .RuleFor(i => i.Id, i => Guid.NewGuid())
                .RuleFor(i => i.CreatedDate,
                    i => i.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                .RuleFor(i => i.FirstName, i => i.Person.FirstName)
                .RuleFor(i => i.LastName, i => i.Person.LastName)
                .RuleFor(i => i.EmailAddress, i => i.Internet.Email())
                .RuleFor(i => i.UserName, i => i.Internet.UserName())
                .RuleFor(i => i.Password, i => PasswordEncryptor.Encrypt(i.Internet.Password()))//burada kendi oluşturduğumuz static sınıftan şifreyi hashliyoruz
                .RuleFor(i => i.EmailConfirmed, i => i.PickRandom(true, false))
                .Generate(500);

            return result;
        }

        public async Task SeedAsync(IConfiguration configuration)
        {
            var dbContextBuilder = new DbContextOptionsBuilder();

            dbContextBuilder.UseSqlServer(configuration["BlazorSozlukDB"]);

            var context = new BlazorSozlukContext(dbContextBuilder.Options);

            var users = GetUsers();
            var userIds = users.Select(i => i.Id);

            await context.Users.AddRangeAsync(users);

            var guids = Enumerable.Range(0, 150).Select(i => Guid.NewGuid()).ToList();
            //burada guid oluşturacağız aşağıda kullanmak için
            int counter = 0;

            var entries = new Faker<Entry>("tr")
                .RuleFor(i => i.Id, i=> guids[counter++])
                .RuleFor(i => i.CreatedDate,
                    i => i.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                .RuleFor(i => i.Subject, i => i.Lorem.Sentence(5, 5))
                .RuleFor(i => i.Content, i => i.Lorem.Paragraph(2))
                .RuleFor(i => i.CreatedById, i => i.PickRandom(userIds))//burada user id ile eşleştiriyoruz aslında.
                .Generate(150);

            await context.Entries.AddRangeAsync(entries);

            var comments = new Faker<EntryComment>("tr")
                .RuleFor(i => i.Id, i=>Guid.NewGuid())
                .RuleFor(i => i.CreatedDate,
                    i => i.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                .RuleFor(i => i.Content, i => i.Lorem.Paragraph(2))
                .RuleFor(i=>i.CreatedById, i=>i.PickRandom(userIds))
                .RuleFor(i => i.EntryId, i => i.PickRandom(guids))//burada guid(entryId) ile eşleştiriyoruz aslında.
                .Generate(1000);

            await context.EntryComments.AddRangeAsync(comments);

            await context.SaveChangesAsync();
        }
    }
}
