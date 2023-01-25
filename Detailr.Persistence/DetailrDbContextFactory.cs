using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Detailr.Persistence
{
    public class DetailrDbContextFactory : IDesignTimeDbContextFactory<DetailrDbContext>
    {
        public DetailrDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<DetailrDbContext>();
            var connectionString = configuration.GetConnectionString("DetailrConnectionString");

            builder.UseSqlServer(connectionString);

            return new DetailrDbContext(builder.Options);
        }
    }
}
