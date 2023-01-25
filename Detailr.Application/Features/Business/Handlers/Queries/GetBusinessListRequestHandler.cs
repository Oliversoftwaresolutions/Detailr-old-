using AutoMapper;
using Detailr.Application.DTOs.Business;
using Detailr.Application.Features.Business.Requests.Queries;
using Detailr.Application.Contracts.Persistence;
using MediatR;

namespace Detailr.Application.Features.Businesses.Handlers.Queries
{
    public class GetBusinessListRequestHandler : IRequestHandler<GetBusinessListRequest, List<BusinessDto>>
    {
        public readonly IBusinessRepository _businessRepository;
        public readonly IMapper _mapper;

        public GetBusinessListRequestHandler(IBusinessRepository businessRepository, IMapper mapper)
        {
            _businessRepository= businessRepository;
            _mapper = mapper;
        }

        public async Task<List<BusinessDto>> Handle(GetBusinessListRequest request, CancellationToken cancellationToken)
        {
            var businesses = await _businessRepository.GetAll();
            return _mapper.Map<List<BusinessDto>>(businesses);
        }
    }
}
