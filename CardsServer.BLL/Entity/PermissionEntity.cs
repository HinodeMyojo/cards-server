namespace CardsServer.BLL.Entity
{
    public class PermissionEntity
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public ICollection<RoleEntity> Roles { get; set; } = [];
    }
}
