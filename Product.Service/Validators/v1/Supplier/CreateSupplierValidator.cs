using FluentValidation;
using Product.Domain.DTOs.Supplier;
using Product.Domain.Error;
using Product.Service.Extensions;

namespace Product.Service.Validators.v1.Supplier
{
    public class CreateSupplierValidator : AbstractValidator<CreateSupplierDTO>
    {
        public CreateSupplierValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Description)
                .Length(3, 30)
                .WithErrorCode(EErrors.SupplierCreateAsyncDescriptionInvalid);


            RuleFor(x => x.NationalRegistration)
                .Length(14)
                .WithErrorCode(EErrors.SupplierCreateAsyncNationalRegistrationInvalid);
        }
    }
}
