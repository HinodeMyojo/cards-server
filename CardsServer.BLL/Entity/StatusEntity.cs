namespace CardsServer.BLL.Entity
{
    /// <summary>
    /// Действует, заблокирован, удален
    /// </summary>
    public class StatusEntity
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public List<UserEntity> Users { get; set; } = [];
    }
}
