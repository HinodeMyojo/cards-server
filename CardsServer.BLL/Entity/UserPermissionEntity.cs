namespace CardsServer.BLL.Entity
{
    public class UserPermissionEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserEntity User { get; set; }
        public int PermissionId { get; set; }
        public PermissionEntity Permission { get; set; }
        public bool isGranted { get; set; }
    }
}