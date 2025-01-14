using CardsServer.BLL.Entity;

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
        }
        public bool IsEmailConfirmed { get; set; }
        public int StatusId { get; set; }
        public int AvatarId { get; set; }
    }
}
