using AutoMapper;
using Detailr.Application.DTOs.Business;
using Detailr.Application.Features.Business.Requests.Queries;
using Detailr.Application.Contracts.Persistence;
using MediatR;

namespace Detailr.Application.Features.Businesses.Handlers.Queries
{
    public class GetBusinessDetailRequestHandler : IRequestHandler<GetBusinessDetailRequest, BusinessDto>
    {
        private readonly IBusinessRepository _businessRepository;
        private readonly IMapper _mapper;

        public GetBusinessDetailRequestHandler(IBusinessRepository businessRepository, IMapper mapper)
        {
            _businessRepository = businessRepository;
            _mapper = mapper;
        }

        public async Task<BusinessDto> Handle(GetBusinessDetailRequest request, CancellationToken cancellationToken)
        {
            var business = await _businessRepository.Get(request.Id);
            return _mapper.Map<BusinessDto>(business);
        }
    }
}
