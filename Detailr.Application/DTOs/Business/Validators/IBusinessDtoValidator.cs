using Detailr.Application.DTOs.Business.Interfaces;
using Detailr.Application.Contracts.Persistence;
using FluentValidation;

namespace Detailr.Application.DTOs.Business.Validators
{
    public class IBusinessDtoValidator : AbstractValidator<IBusinessDto>
    {
        private readonly IBusinessRepository _businessRepository;

        public IBusinessDtoValidator(IBusinessRepository businessRepository)
        {
            _businessRepository = businessRepository;


            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("Business {PropertyName} is required")
                .NotNull()
                .MaximumLength(255).WithMessage("Business {propertyName} must not exceed {ComparaisonValue} characters.")
                .MustAsync(async (name, token) =>
                {
                    var nameExists = await _businessRepository.DoesBusinessNameExist(name);
                    return !nameExists;
                })
                .WithMessage("{PropertyName} already exists");

            RuleFor(b => b.Description)
                .MaximumLength(255).WithMessage("Business {PropertyName} must not exceed {ComparaisonValue} characters");
        }
    }
}
