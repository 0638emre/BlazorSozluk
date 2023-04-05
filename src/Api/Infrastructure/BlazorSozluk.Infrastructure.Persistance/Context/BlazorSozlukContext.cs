using System.Reflection;
using BlazorSozluk.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorSozluk.Infrastructure.Persistance.Context
{
    public class BlazorSozlukContext : DbContext
    {
        public const string DEFAULT_SCHEMA = "dbo";

        public BlazorSozlukContext()
        {
                
        }
        public BlazorSozlukContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Entry> Entries { get; set; }

        public DbSet<EntryVote> EntryVotes { get; set; }
        public DbSet<EntryFavorite> EntryFavorites { get; set; }

        public DbSet<EntryComment> EntryComments { get; set; }
        public DbSet<EntryCommentVote> EntryCommentVotes { get; set; }
        public DbSet<EntryCommentFavorite> EntryCommentFavorites { get; set; }

        public DbSet<EmailConfirmation> EmailConfirmations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //db context program ilk ayağa kalktığında program.cs tarafından oluşturulduğu için burada böyle bir mantık veriyoruz. eğer configüre edilmediyse.
            if (!optionsBuilder.IsConfigured)
            {
                var connStr = "Data Source = localhost;Initial Catalog=BlazorSozlukDB;Integrated Security=true;TrustServerCertificate=True";

                optionsBuilder.UseSqlServer(connStr, opt =>
                {
                    opt.EnableRetryOnFailure(); //veritabanına bağlanırken hata alırsan yeniden dene.
                });
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //fluent api ile modeller üzerinde ilişkileri bu assembly içerisinde farklı classlarda yapacağımız için bu satırda bunu veriyoruz. Tek tek de verilebilirdi fakat bu extension class ile daha basit yapmış oluyoruz
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        #region SaveChangesMetotlarının CreatedDate'i setlemesi

        //aşağıda bütün save changes ile ilgili metotları override ediyoruz ki baseentityden gelen createdDate alanını her kayıt işleminde manuel vermeyelim.
        public override int SaveChanges()
        {
            OnBeforeSave();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSave();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            OnBeforeSave();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            OnBeforeSave();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        //bu metot ile kayıttan önceki ele alınan nesnenin bir Ekleme yapacağını alıyoruz.
        private void OnBeforeSave()
        {
            var addedEntities = ChangeTracker.Entries()
                .Where(i => i.State == EntityState.Added)
                .Select(i => (BaseEntity)i.Entity);

            PrepareAddedEntities(addedEntities);
        }

        private void PrepareAddedEntities(IEnumerable<BaseEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (entity.CreatedDate == DateTime.MinValue)//nullable olmadığı için setlenmediyse min value değerini alır
                {
                    entity.CreatedDate = DateTime.Now;
                }
            }
        }

        #endregion

    }
}
