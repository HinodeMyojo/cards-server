using CardsServer.BLL.Entity;
using System.Linq.Expressions;

namespace CardsServer.BLL.Abstractions
{
    public interface IUserRepository
    {
        Task<UserEntity?> GetUser(Expression<Func<UserEntity, bool>> predicate, CancellationToken cancellationToken);
        Task EditUser(UserEntity user, CancellationToken cancellationToken);
        //Task<UserEntity?> GetUserByEmail(string email, CancellationToken cancellationToken);
        //Task<UserEntity?> GetUserByUserName(string userName, CancellationToken cancellationToken);
        //Task<UserEntity?> GetUser(int userId, CancellationToken cancellationToken);
    }
}
