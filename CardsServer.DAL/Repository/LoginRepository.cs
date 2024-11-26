using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Dto.Login;
using CardsServer.BLL.Entity;
using Microsoft.EntityFrameworkCore;

namespace CardsServer.DAL.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly ApplicationContext _context;

        public LoginRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<UserEntity?> GetUser(LoginUser user, CancellationToken cancellationToken)
        {
            UserEntity? result = await _context.Users
                .Include(u => u.Role)
                .ThenInclude(x => x.Permissions)
                .Include(x => x.RefreshTokens)
                .SingleOrDefaultAsync(x => x.UserName == user.UserName);
            return result;
        }

        public async Task RegisterUser(UserEntity model, CancellationToken cancellationToken)
        {
            await _context.Users.AddAsync(model);
            await _context.SaveChangesAsync();
        }
    }
}
