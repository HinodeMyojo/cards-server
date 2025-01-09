using CardsServer.BLL.Dto.User;
using CardsServer.BLL.Entity;

namespace CardsServer.BLL.Dto.Profile;

public class GetProfileSimpleAccess : GetUserSimpleResponse
{
    public bool CanViewProfile { get; set; }
    public bool CanBlockUser { get; set; }
    public bool CanDeleteUser { get; set; }
    public bool CanEditUser { get; set; }
    /// <summary>
    /// Этот профиль принадлжеит запросившему пользователю?
    /// </summary>
    public bool IsUserProfile { get; set; }

    public static explicit operator GetProfileSimpleAccess(UserEntity userEntity)
    {
        GetUserSimpleResponse user = (GetUserSimpleResponse) userEntity;

        return new GetProfileSimpleAccess()
        {
            Id = user.Id,
            Avatar = user.Avatar,
            UserName = user.UserName,
            AvatarId = user.AvatarId,
            IsEmailConfirmed = user.IsEmailConfirmed,
            RoleId = user.RoleId,
            StatusId = user.StatusId,
        };
    }
}