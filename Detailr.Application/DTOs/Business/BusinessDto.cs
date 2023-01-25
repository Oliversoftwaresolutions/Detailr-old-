using Detailr.Application.DTOs.Business.Interfaces;
using Detailr.Application.DTOs.Common;

namespace Detailr.Application.DTOs.Business
{
    public class BusinessDto : BaseDto, IBusinessDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
