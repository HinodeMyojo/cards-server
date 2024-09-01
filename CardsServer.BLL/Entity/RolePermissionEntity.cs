namespace CardsServer.BLL.Entity
{
    public class RolePermissionEntity
    {
        public int RoleId { get; set; }
        public int PermissionId {  get; set; }
        public RoleEntity? Role { get; set; }
        public PermissionEntity? Permission { get; set; }
    }
}
