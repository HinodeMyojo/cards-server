namespace CardsServer.BLL.Dto.User
{
    public class GetAvatarResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public required string Avatar { get; set; }
    }
}
