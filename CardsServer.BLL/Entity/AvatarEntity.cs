namespace CardsServer.BLL.Entity
{
    public class AvatarEntity
    {
        /// Если аватара нет - ставится дефолтный 0
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserEntity? User { get; set; }
        public required string Avatar {  get; set; }
    }
}
