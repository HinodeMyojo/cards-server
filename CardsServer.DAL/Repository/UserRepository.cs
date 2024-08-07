using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Dto.User;
using CardsServer.BLL.Entity;

namespace CardsServer.DAL.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<GetUserResponse> GetUser(int userId, CancellationToken cancellationToken)
        {
            UserEntity? user = await _context.Users.FindAsync(userId, cancellationToken);
        }
    }
}
