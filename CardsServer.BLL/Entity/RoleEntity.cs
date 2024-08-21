namespace CardsServer.BLL.Entity
{
    public class RoleEntity
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public ICollection<PermissionEntity> Permissions { get; set; }
        public List<UserEntity> Users { get; set; } = [];
    }
}
