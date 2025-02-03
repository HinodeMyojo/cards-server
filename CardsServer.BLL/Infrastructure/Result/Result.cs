using CardsServer.BLL.Dto.Module;
using System.Net;

namespace CardsServer.BLL.Infrastructure.Result
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public Error Error { get; set; }
        public int StatusCode { get; set; }

        protected Result(bool isSuccess, Error error)
        {
            IsSuccess = isSuccess;
            Error = error;
            StatusCode = Error.StatusCode;
        }

        public static Result Success(string? message) => new(true, new Error(message, HttpStatusCode.OK));
        public static Result Success() => new(true, Error.None);
        public static Result Failure(Error error) => new(false, error);
        public static Result Failure(string message) => new(false, new Error(message, HttpStatusCode.BadRequest));
    }

    public class Result<T> : Result
    {
        public T Value { get; set; }

        private Result(T value, bool isSuccess, Error error) : base(isSuccess, error)
        {
            Value = value;
        }

        public static Result<T> Success(T value) => new(value, true, Error.None);
        public static Result<T> Success() => new(default, true, Error.None);
        public static new Result<T> Failure(Error error) => new(default!, false, error);
        public static new Result<T> Failure(string message) => new(default!, false, new Error(message, HttpStatusCode.BadRequest));

        //public static implicit operator Result(Result<T> result)
        //{
        //    return new Result(result.IsSuccess, result.Error);
        //}
    }
}
