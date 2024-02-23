using FluentValidation;
using Product.Domain.DTOs.Product;
using Product.Domain.Error;
using Product.Service.Extensions;

namespace Product.Service.Validators.v1.Product
{
    public class CreateProductValidator : AbstractValidator<CreateProductDTO>
    {
        public CreateProductValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Description)
                .Length(3, 100)
                .WithErrorCode(EErrors.ProductCreateAsyncDescriptionInvalid);

            RuleFor(x => x.ManufacturingDate)
                .LessThan(x => x.ExpirationDate)
                .WithErrorCode(EErrors.ProductCreateAsyncManufacturingDateInvalid);

        }

    }
}
