using Detailr.Application.DTOs.Business;
using MediatR;

namespace Detailr.Application.Features.Business.Requests.Queries
{
    public class GetBusinessListRequest : IRequest<List<BusinessDto>>
    {

    }
}
