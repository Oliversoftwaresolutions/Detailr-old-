using AutoMapper;
using Detailr.Application.DTOs.Business.Validators;
using Detailr.Application.Exceptions;
using Detailr.Application.Features.Business.Requests.Commands;
using Detailr.Application.Contracts.Persistence;
using Detailr.Application.Responses;
using MediatR;

namespace Detailr.Application.Features.Business.Handlers.Commands
{
    public class CreateBusinessCommandHandler : IRequestHandler<CreateBusinessCommand, BaseCommandResponse>
    {
        private readonly IBusinessRepository _businessRepository;
        private readonly IMapper _mapper;

        public CreateBusinessCommandHandler(IBusinessRepository businessRepository, IMapper mapper)
        {
            _businessRepository = businessRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateBusinessCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateBusinessDtoValidator(_businessRepository);
            var validationResult = await validator.ValidateAsync(request.CreateBusinessDto);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Creation failed";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            }
         
            var business = _mapper.Map<Domain.Business>(request.CreateBusinessDto);

            business = await _businessRepository.Add(business);

            response.Success = true;
            response.Message = "Creation successful";
            response.Id = business.Id;

            return response;
        }
    }
}
