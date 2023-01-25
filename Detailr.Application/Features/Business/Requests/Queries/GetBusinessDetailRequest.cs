using Detailr.Application.DTOs.Business;
using MediatR;

namespace Detailr.Application.Features.Business.Requests.Queries
{
    public class GetBusinessDetailRequest : IRequest<BusinessDto>
    {
        public int Id { get; set; }
    }
}
