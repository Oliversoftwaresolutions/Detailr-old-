using Detailr.Application.Contracts.Persistence;
using Detailr.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Detailr.Persistence
{
    public static class PersistenceServicesRegistration
    {
        public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DetailrDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DetailrConnectionString"));
            });

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IBusinessRepository, BusinessRepository>();
           
            return services;
        }
    }
}
