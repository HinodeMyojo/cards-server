using CardsServer.BLL.Dto.User;

namespace CardsServer.BLL.Dto.Profile;

public class GetProfileAccess : GetUserSimpleResponse
{
    public GetProfileAccess(GetUserSimpleResponse user) 
        : base(user.Id, user.UserName, user.Avatar, user.HasPrivateProfile, user.RoleId)
    {
    }

    public bool CanViewProfile { get; set; }
    public bool CanBlockUser { get; set; }
    public bool CanDeleteUser { get; set; }
    public bool CanEditUser { get; set; }
    public bool IsUserProfile { get; set; }
}