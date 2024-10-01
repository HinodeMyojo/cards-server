using System.Net;

namespace CardsServer.BLL.Infrastructure.Result
{
    public class Error
    {
        public string? Message { get; set; }
        public int StatusCode { get; set; }
        //public static Error None = new(null, HttpStatusCode.OK);
        public Error()
        {}


        public Error(HttpStatusCode statusCode)
        {
            this.Message = null;
            this.StatusCode = (int)statusCode;
        }
        public Error(string? message, int statusCode = 400)
        {
            this.Message = message;
            this.StatusCode = statusCode;
        }

        public Error(string? message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            this.Message = message;
            this.StatusCode = (int)statusCode;
        }

        public static readonly Error None = new(HttpStatusCode.OK);

    }
}
