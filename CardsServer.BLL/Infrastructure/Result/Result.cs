namespace CardsServer.BLL.Infrastructure.Result
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public string Error { get; set; }
        public int StatusCode { get; set; }


        protected Result(bool isSuccess, string error, int statusCode)
        {
            IsSuccess = isSuccess;
            Error = error;
            StatusCode = statusCode;
        }

        public static Result Success(int statusCode = 200) => new(true, string.Empty, statusCode);
        public static Result Failure(string error, int statusCode = 400) => new(false, error, statusCode);
    }

    public class Result<T> : Result
    {
        public T Value { get; set; }

        private Result(T value, bool isSuccess, string error, int statusCode) : base(isSuccess, error, statusCode)
        {
            Value = value;
        }

        public static Result<T> Success(T value, int statusCode = 200) => new(value, true, string.Empty, statusCode);
        public static new Result<T> Failure(string error, int statusCode = 400) => new(default!, false, error, statusCode);
    }
}
