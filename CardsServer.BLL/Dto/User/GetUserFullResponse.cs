using CardsServer.BLL.Entity;
using CardsServer.BLL.Enums;

namespace CardsServer.BLL.Dto.User
{
    public class GetUserFullResponse : GetUserSimpleResponse
    {
        public GetUserFullResponse()
        {
            
        }

        public GetUserFullResponse(UserEntity user) : base(user)
        {
            IsEmailConfirmed = user.IsEmailConfirmed;
            StatusId = user.StatusId;
            AvatarId = user.AvatarId;
            IsAdmin = user.RoleId is (int)RoleEnum.Admin or (int)RoleEnum.Moderator;
        }
        public bool IsEmailConfirmed { get; set; }
        public int StatusId { get; set; }
        public int AvatarId { get; set; }
        public bool IsAdmin { get; set; }
    }
}
