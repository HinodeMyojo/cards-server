using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Dto;
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

        public async Task RegisterUser(UserEntity model, CancellationToken cancellationToken)
        {
            await _context.Users.AddAsync(model);
            await _context.SaveChangesAsync();
        }
    }
}
