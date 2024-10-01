namespace CardsServer.BLL.Entity
{
    public class ImageEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserEntity? User { get; set; }
        public required string ImageUrl { get; set; }
    }
}
