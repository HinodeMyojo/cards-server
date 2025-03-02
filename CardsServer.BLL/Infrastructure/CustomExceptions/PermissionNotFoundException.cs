namespace CardsServer.BLL.Infrastructure.CustomExceptions
{
    public class PermissionNotFoundException : Exception
    {
        public PermissionNotFoundException() : base()
        {
        }

        public PermissionNotFoundException(string message) : base(message)
        {
            
        }
    }
}