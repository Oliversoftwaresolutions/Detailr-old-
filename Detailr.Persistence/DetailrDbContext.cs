using Detailr.Domain;
using Detailr.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Detailr.Persistence
{
    public class DetailrDbContext : DbContext
    {
        public DetailrDbContext(DbContextOptions<DetailrDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DetailrDbContext).Assembly);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseDomainEntity>())
            {
                entry.Entity.LastModifiedDate = DateTime.Now;

                if (entry.State == EntityState.Added)
                    entry.Entity.DateCreated= DateTime.Now;


            }
            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<Business> Businesses { get; set; }
    }
}
