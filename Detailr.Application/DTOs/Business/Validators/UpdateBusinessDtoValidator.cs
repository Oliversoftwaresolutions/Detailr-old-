using Detailr.Application.Contracts.Persistence;
using FluentValidation;

namespace Detailr.Application.DTOs.Business.Validators
{
    public class UpdateBusinessDtoValidator : AbstractValidator<UpdateBusinessDto>
    {
        private readonly IBusinessRepository _businessRepository;

        public UpdateBusinessDtoValidator(IBusinessRepository businessRepository)
        {
            _businessRepository = businessRepository;

            Include(new IBusinessDtoValidator(_businessRepository));

            RuleFor(x => x.Id).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
