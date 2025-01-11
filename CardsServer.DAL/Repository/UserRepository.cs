using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CardsServer.DAL.Repository
{
    public sealed class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<UserEntity?> GetUser(Expression<Func<UserEntity, bool>> predicate,
            CancellationToken cancellationToken)
        {
            UserEntity? user = await _context
                .Users
                .Include(x => x.UserModules)
                .Include(x => x.Avatar)
                .Include(x => x.RefreshTokens)
                .Include(x => x.Role)
                .Include(x => x.Avatar)
                .FirstOrDefaultAsync(predicate, cancellationToken);

            return user;
        }
        
        public async Task EditUser(UserEntity user, CancellationToken cancellationToken)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync(cancellationToken);
        }
        
        /*
        public async Task<UserEntity?> GetUser(int userId, CancellationToken cancellationToken)
        {
            UserEntity? user = await _context
                .Users
                .Include(x => x.UserModules)
                .Include(x => x.Avatar)
                .Include(x => x.RefreshTokens)
                .Include(x => x.Role)
                .Include(x => x.Avatar)
                .FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);

            return user;
        }

        public async Task<UserEntity?> GetUserByEmail(string email, CancellationToken cancellationToken)
        {
            UserEntity? user = await _context
                .Users
                .Include(x => x.UserModules)
                .Include(x => x.Avatar)
                .Include(x => x.RefreshTokens)
                .Include(x => x.Role)
                .Include(x => x.Avatar)
                .FirstOrDefaultAsync(x => x.Email == email, cancellationToken);

            return user;
        }

        public async Task<UserEntity?> GetUserByUserName(string userName, CancellationToken cancellationToken)
        {
            UserEntity? user = await _context
                .Users
                .Include(x => x.UserModules)
                .Include(x => x.Avatar)
                .Include(x => x.RefreshTokens)
                .Include(x => x.Role)
                .Include(x => x.Avatar)
                .FirstOrDefaultAsync(x => x.UserName == userName, cancellationToken);

            return user;
        }*/
        }
    }
