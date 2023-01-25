using AutoMapper;
using Detailr.MVC.Models;
using Detailr.MVC.Services.Base;

namespace Detailr.MVC
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBusinessDto, CreateBusinessVM>().ReverseMap();
            CreateMap<BusinessDto, BusinessVM>().ReverseMap();
        }
    }
}
