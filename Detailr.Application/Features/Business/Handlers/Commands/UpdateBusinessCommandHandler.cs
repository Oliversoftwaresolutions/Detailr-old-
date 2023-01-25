using AutoMapper;
using Detailr.Application.DTOs.Business;
using Detailr.Application.DTOs.Business.Validators;
using Detailr.Application.Exceptions;
using Detailr.Application.Features.Business.Requests.Commands;
using Detailr.Application.Contracts.Persistence;
using MediatR;

namespace Detailr.Application.Features.Businesses.Handlers.Commands
{
    public class UpdateBusinessCommandHandler : IRequestHandler<UpdateBusinessCommand, Unit>
    {
        private readonly IBusinessRepository _businessRepository;
        private readonly IMapper _mapper;

        public UpdateBusinessCommandHandler(IBusinessRepository businessRepository, IMapper mapper)
        {
            _businessRepository = businessRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateBusinessCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateBusinessDtoValidator(_businessRepository);
            var validationResult = await validator.ValidateAsync(request.UpdateBusinessDto);

            if (validationResult.IsValid)
                throw new ValidationException(validationResult);

            var business = await _businessRepository.Get(request.UpdateBusinessDto.Id);

            _mapper.Map(request.UpdateBusinessDto, business);

            await _businessRepository.Update(business);

            return Unit.Value;
        }
    }
}
