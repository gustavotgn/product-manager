using FluentValidation;
using Product.Domain.Error;
using Product.Domain.Extensions;

namespace Product.Service.Extensions
{
    public static class RuleBuilderOptionsExntension
    {
        public static IRuleBuilderOptions<T, TProperty> WithErrorCode<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule, EErrors enumError) =>
            rule.WithErrorCode(enumError.ToErrorCode()).WithMessage(enumError.Description());
    }
}
