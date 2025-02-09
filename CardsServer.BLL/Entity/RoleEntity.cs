namespace CardsServer.BLL.Entity
{
    public class RoleEntity
    {
        public required int Id { get; init; }
        public required string Name { get; init; }
        public ICollection<PermissionEntity> Permissions { get; init; } = [];
        public ICollection<RolePermissionEntity> RolePermissions { get; init; } = [];
        public ICollection<UserEntity> Users { get; init; } = [];
    }
}
