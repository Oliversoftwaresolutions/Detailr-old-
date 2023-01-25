using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Detailr.Identity
{
    public class DetailrIdentityDbContextFactory : IDesignTimeDbContextFactory<DetailrIdentityDbContext>
    {
        public DetailrIdentityDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<DetailrIdentityDbContext>();
            var connectionString = configuration.GetConnectionString("DetailrIdentityConnectionString");

            builder.UseSqlServer(connectionString);

            return new DetailrIdentityDbContext(builder.Options);
        }
    }
}
