using CardsServer.BLL.Entity;

namespace CardsServer.BLL.Dto.User
{
    public class GetUserSimpleResponse
    {
        public int Id { get; set; }
        public required string UserName { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public int StatusId { get; set; }
        public int RoleId { get; set; }
        public int AvatarId { get; set; }
        public required string Avatar { get; set; }

        public static explicit operator GetUserSimpleResponse(UserEntity user)
        {
            return new GetUserSimpleResponse
            {
                Id = user.Id,
                UserName = user.UserName,
                AvatarId = user.AvatarId,
                IsEmailConfirmed = user.IsEmailConfirmed,
                RoleId = user.RoleId,
                StatusId = user.StatusId,
                Avatar = user.Avatar == null ? "" : Convert.ToBase64String(user.Avatar.Data)
            };
        }
    }
}
