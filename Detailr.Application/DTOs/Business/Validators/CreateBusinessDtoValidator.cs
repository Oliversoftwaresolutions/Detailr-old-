using Detailr.Application.Contracts.Persistence;
using FluentValidation;

namespace Detailr.Application.DTOs.Business.Validators
{
    public class CreateBusinessDtoValidator : AbstractValidator<CreateBusinessDto>
    {
        private readonly IBusinessRepository _businessRepository;

        public CreateBusinessDtoValidator(IBusinessRepository businessRepository)
        {
            _businessRepository = businessRepository;

            Include(new IBusinessDtoValidator(_businessRepository));
        }
    }
}
