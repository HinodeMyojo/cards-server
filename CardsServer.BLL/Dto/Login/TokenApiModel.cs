namespace CardsServer.BLL.Dto.Login
{
    public class TokenApiModel
    {
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
    }
}
