namespace CardsServer.BLL.Infrastructure.CustomExceptions
{
    public class NotSelectedValidatorException : Exception
    {
        public NotSelectedValidatorException(string? message) : base(message)
        {
        }

        public NotSelectedValidatorException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
