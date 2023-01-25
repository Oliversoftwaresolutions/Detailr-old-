using Detailr.Identity.Configurations;
using Detailr.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Detailr.Identity
{
    public class DetailrIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
       public DetailrIdentityDbContext(DbContextOptions<DetailrIdentityDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
