using Detailr.Application.DTOs.Business.Interfaces;
using Detailr.Application.DTOs.Common;

namespace Detailr.Application.DTOs.Business
{
    public class CreateBusinessDto : BaseDto, IBusinessDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
