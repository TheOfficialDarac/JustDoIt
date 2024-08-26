using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace JustDoIt.Common
{
    public class Result
    {
        private Result(bool isSuccess, IEnumerable<Error> errors)
        {
            if ((isSuccess && errors.Any()) ||
                (!isSuccess && !errors.Any()))
            {
                throw new ArgumentException("Invalid error", nameof(errors));
            }

            IsSuccess = isSuccess;
            Errors = errors;
        }

        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;

        public IEnumerable<Error> Errors { get; }

        public static Result Success() => new(true, Enumerable.Empty<Error>());

        public static Result Failure(IEnumerable<Error> errors) => new(false, errors);
    }
}
