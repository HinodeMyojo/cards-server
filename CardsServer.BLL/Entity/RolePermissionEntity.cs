namespace CardsServer.BLL.Entity
{
    public class RolePermissionEntity
    {
        public required int RoleId { get; set; }
        public required int PermissionId {  get; set; }
        public RoleEntity? Role { get; set; }
        public PermissionEntity? Permission { get; set; }

    }
}
