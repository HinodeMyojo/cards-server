using CardsServer.BLL.Entity;
using CardsServer.BLL.Enums;
using System.Diagnostics.CodeAnalysis;

namespace CardsServer.BLL.Dto.User
{
    public class GetUserSimpleResponse : GetBaseUserResponse
    {
        protected GetUserSimpleResponse()
        {
            
        }
        public GetUserSimpleResponse(UserEntity user)
        {
            Id = user.Id;
            UserName = user.UserName;
            Avatar = user.Avatar == null ? "" : Convert.ToBase64String(user.Avatar.Data);
            HasPrivateProfile = user.Profile.IsPrivate;
            RoleId = user.RoleId;
        }

        protected GetUserSimpleResponse(int id, string userName, string avatar, bool hasPrivateProfile, int roleId)
        {
            Id = id;
            UserName = userName;
            Avatar = avatar;
            HasPrivateProfile = hasPrivateProfile;
            RoleId = roleId;
        }


        public int Id { get; set; }
        public string UserName { get; set; }
        public string Avatar { get; set; }
        public bool HasPrivateProfile { get; set; }
        public int RoleId { get; set; }

        public static explicit operator GetUserSimpleResponse(UserEntity user)
        {
            try
            {
                return new GetUserSimpleResponse(user);
            }
            catch(Exception ex)
            {
                throw new Exception($"Не удалось преобразовать модель пользователя! {ex.Message}");
            }
        }
    }
}
