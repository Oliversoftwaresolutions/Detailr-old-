using AutoMapper;
using Detailr.Application.DTOs.Business;
using Detailr.Domain;

namespace Detailr.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Business, BusinessDto>().ReverseMap();
            CreateMap<Business, CreateBusinessDto>().ReverseMap();
            CreateMap<Business, UpdateBusinessDto>().ReverseMap();
        }
    }
}
