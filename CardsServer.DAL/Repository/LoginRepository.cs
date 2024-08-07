using CardsServer.BLL.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace CardsServer.DAL.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly DbContext _context;

        public LoginRepository(DbContext context)
        {
            _context = context;
        }
    }
}
