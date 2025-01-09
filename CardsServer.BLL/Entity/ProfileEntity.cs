namespace CardsServer.BLL.Entity
{
    public class ProfileEntity
    {
        public UserEntity User { get; set; }
        public int UserId { get; set; }
        public bool IsPrivate { get; set; }
    }
}
