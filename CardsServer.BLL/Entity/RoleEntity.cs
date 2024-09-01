namespace CardsServer.BLL.Entity
{
    public class RoleEntity
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public ICollection<PermissionEntity> Permissions { get; set; } = [];
        public ICollection<RolePermissionEntity> RolePermissions { get; set; } = [];
        public ICollection<UserEntity> Users { get; set; } = [];
    }
}
