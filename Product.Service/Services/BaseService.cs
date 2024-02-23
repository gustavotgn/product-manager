using FluentValidation.Results;
using Product.Domain.Error;
using Product.Domain.Utils;
using System.Collections.Generic;
using System.Linq;

namespace Product.Service.Services
{
    public abstract class BaseService
    {
        protected static Result Success()
            => Result.WithSuccess();

        protected static Result Error(params Error[] error)
            => Result.WithError(error);

        protected static Result Error(List<ValidationFailure> errors)
            => Result.WithError(errors.Select(x => new Error(x.ErrorCode, x.ErrorMessage)).ToArray());

        protected static Result<T> Success<T>(T data) where T : class
            => Result<T>.WithSuccess(data);

        protected static Result Error(EErrors error)
            => Error(new Error(error));

        protected static Result<T> Error<T>(params Error[] error) where T : class
            => Result<T>.WithError(error);

        protected static Result<T> Error<T>(List<ValidationFailure> errors) where T : class
            => Result<T>.WithError(errors.Select(x => new Error(x.ErrorCode, x.ErrorMessage)).ToArray());

        protected static Result<T> Error<T>(EErrors error) where T : class
            => Error<T>(new Error(error));
    }
}
