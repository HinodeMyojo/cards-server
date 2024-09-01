using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Dto.User;
using CardsServer.BLL.Entity;

namespace CardsServer.BLL.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetUserResponse> GetUser(int userId, CancellationToken cancellationToken)
        {
            UserEntity res = await _repository.GetUser(userId, cancellationToken);

            GetUserResponse result = new()
            {
                Id = res.Id,
                Email = res.Email,
                StatusId = res.StatusId,
                AvatarId = res.Avatar.Id,
                UserName = res.UserName,
                IsEmailConfirmed = res.IsEmailConfirmed,
                RoleId = res.RoleId,
            };

            return result;

        }
    }
}
