namespace CardsServer.BLL.Infrastructure.Auth
{
    public class JwtOptions
    {
        public required string SecretKey { get; set; }
        public required string Issuer { get; set; }
        public int ExpiresMinutes { get; set; }
    }
}
