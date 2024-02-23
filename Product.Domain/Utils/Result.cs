using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Product.Domain.Utils
{
    public class Result<T> : Result where T : class
    {
        protected Result(bool success, T data) : base(success)
            => Data = data;

        protected Result(params Error[] error) : base(error)
            => Errors = error;

        public T? Data { get; set; }

        public static Result<T> WithSuccess(T data) => new(true, data);

        public new static Result<T> WithError(params Error[] error) => new(error);

    }

    public class Result
    {
        protected Result(bool success)
            => Success = success;

        protected Result(params Error[] error)
        {
            Success = false;
            Errors = error;
        }

        public bool Success { get; protected set; }

        public IEnumerable<Error> Errors { get; protected set; }

        [JsonIgnore]
        public Error? Error => Errors?.FirstOrDefault();

        public static Result WithSuccess() => new(true);

        public static Result WithError(params Error[] error) => new(error);

    }
}
