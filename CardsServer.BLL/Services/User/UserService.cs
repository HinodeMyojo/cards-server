using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Dto.User;

namespace CardsServer.BLL.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository
            )
        {
            _repository = repository;
        }

        public async Task<GetUserResponse> GetUser(int userId, CancellationToken cancellationToken)
        {
            return await _repository.GetUser(userId, cancellationToken);
        }
    }
}
