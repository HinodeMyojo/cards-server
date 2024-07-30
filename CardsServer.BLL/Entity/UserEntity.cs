namespace CardsServer.BLL.Entity
{
    public class UserEntity
    {
        public int Id { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public int StatusId { get; set; }
        public required StatusEntity Status { get; set; }
        public int RoleId { get; set; }
        public required RoleEntity Role { get; set; }
        public required AvatarEntity Avatar { get; set; }

    }
}
