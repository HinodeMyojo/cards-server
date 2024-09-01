namespace CardsServer.BLL.Infrastructure.CustomExceptions
{
    public class ConnectionException : Exception
    {
        public ConnectionException(string? message = null, Exception? innerException = null) : base(message, innerException) { }
    }
}
