using CardsServer.BLL.Entity;

namespace CardsServer.BLL.Dto.User
{
    public class GetUserResponse
    {
        public int Id { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public int StatusId { get; set; }
        public int RoleId { get; set; }
        public int AvatarId { get; set; }
        public required string Avatar { get; set; }
    }
}
