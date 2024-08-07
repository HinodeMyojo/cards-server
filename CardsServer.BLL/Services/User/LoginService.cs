using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Dto;

namespace CardsServer.BLL.Services.User
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;

        public LoginService(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        public Task RegisterUser(RegisterUser model, CancellationToken cancellationToken)
        {
            
        }

    }
}
