namespace CardsServer.BLL.Infrastructure.Result
{
    public class Result<T>
    {
        public T Value { get; set; }
        public bool IsSuccess { get; set; }
        public string Error { get; set; }
        public int StatusCode { get; set; }


        private Result(T value, bool isSuccess, string error, int statusCode)
        {
            Value = value;
            IsSuccess = isSuccess;
            Error = error;
            StatusCode = statusCode;
        }

        public static Result<T> Success(T value, int statusCode = 200) => new(value, true, string.Empty, statusCode);
        public static Result<T> Failure(string error, int statusCode = 400) => new(default!, false, error, statusCode);
    }
}
