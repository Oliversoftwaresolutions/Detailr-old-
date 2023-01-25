using AutoMapper;
using Detailr.Application.Exceptions;
using Detailr.Application.Features.Business.Requests.Commands;
using Detailr.Application.Contracts.Persistence;
using MediatR;

namespace Detailr.Application.Features.Businesses.Handlers.Commands
{
    public class DeleteBusinessCommandHandler : IRequestHandler<DeleteBusinessCommand>
    {
        private readonly IBusinessRepository _businessRepository;
        private readonly IMapper _mapper;

        public DeleteBusinessCommandHandler(IBusinessRepository businessRepository, IMapper mapper)
        {
            _businessRepository = businessRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteBusinessCommand request, CancellationToken cancellationToken)
        {
            var business = await _businessRepository.Get(request.Id);

            if (business == null)
                throw new NotFoundException(nameof(business), request.Id);

            await _businessRepository.Delete(business);

            return Unit.Value;
        }
    }
}
