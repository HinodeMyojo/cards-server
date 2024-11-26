namespace CardsServer.BLL.Dto.Login
{
    public class LoginUser
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }

    public class LoginUserExtension : LoginUser
    {
        public string? FingerPrint { get; set; }
    }
}
