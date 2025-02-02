namespace CardsServer.BLL.Entity
{
    public class PermissionEntity
    {
        public required int Id { get; init; }
        public string? Title { get; init; }
        public string? Description { get; init; }
        public ICollection<RoleEntity> Roles { get; init; } = [];
        public ICollection<RolePermissionEntity> RolePermissions { get; init; } = [];
    }
}
