namespace CardsServer.BLL.Entity
{
    public class AvatarEntity
    {
        /// Если аватара нет - ставится дефолтный 0
        public int Id { get; set; }
        public List<UserEntity> Users { get; set; } = [];
        public required byte[] Data { get; set; }
    }
}
