namespace CardsServer.BLL.Entity
{
    public class RefreshTokenEntity
    {
        public int Id { get; set; }
        public required string Token {  get; set; }
        public required string FingerPrint { get; set; }
        public required DateTime RefreshTokenExpiryTime { get; set; }
        public int UserId {  get; set; }
        public UserEntity? User {  get; set; }
    }
}
