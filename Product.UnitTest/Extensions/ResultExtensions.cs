using Product.Domain.Error;
using Product.Domain.Extensions;
using Product.Domain.Utils;
using System;
using System.Linq;

namespace Product.UnitTest.Extensions
{
    public static class ResultExtensions
    {
        public static EErrors GetEnumError(this Result result) =>
            (EErrors) Convert.ToInt32(result?.Error?.Code);

        public static bool IsError(this Result resultData, EErrors error) =>
            resultData.Errors.Any(x => x.Code.Equals(error.ToErrorCode()) && x.Message.Equals(error.Description()));
    }
}
