using Detailr.Domain.Common;

namespace Detailr.Domain
{
    public class Business : BaseDomainEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
