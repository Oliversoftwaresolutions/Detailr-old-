using Detailr.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Detailr.Persistence.Configurations.Entities
{
    public class BusinessConfiguration : IEntityTypeConfiguration<Business>
    {
        public void Configure(EntityTypeBuilder<Business> builder)
        {
            builder.HasData(new Business { Id = 1, Name = "Test Business 1", Description = "Test Business 1 Description" });
            builder.HasData(new Business
            { 
                Id = 2, Name = "Test Business 2",
                Description = "Test Business 2 Description",
                Latitude = 51.507969111981254,
                Longitude = -0.021353916973630996 
            });

            builder.HasData(new Business
            {
                Id = 4,
                Name = "Test Business 3",
                Description = "Test Business 3 Description",
                Latitude = 51.50888328081055,
                Longitude = -0.07323854928618917
            });

            builder.HasData(new Business
            {
                Id = 5,
                Name = "Test Business 4",
                Description = "Test Business 4 Description",
                Latitude = 51.45880520353958,
                Longitude = -0.10797751340028962
            });

            builder.HasData(new Business
            {
                Id = 6,
                Name = "Test Business 5",
                Description = "Test Business 5 Description",
                Latitude = 51.76718770166983,
                Longitude = -0.3409916280412093
            });
        }
    }
}
