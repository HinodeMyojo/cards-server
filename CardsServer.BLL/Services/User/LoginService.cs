using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Dto;
using CardsServer.BLL.Dto.Login;
using CardsServer.BLL.Entity;
using CardsServer.BLL.Infrastructure;

namespace CardsServer.BLL.Services.User
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;

        public LoginService(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        public async Task<string> LoginUser(LoginUser user, CancellationToken cancellationToken)
        {
            UserEntity? res = await _loginRepository.GetUser(user, cancellationToken);
            if (PasswordExtension.CheckPassword(res.Password, user.Password))
            {
                
            }
            AssertModel.CheckNull(res);
        }

        public async Task RegisterUser(RegisterUser model, CancellationToken cancellationToken)
        {

            UserEntity user = new()
            {
                Email = model.Email,
                Password = model.Password,
                IsEmailConfirmed = false,
                RoleId = 1,
                StatusId = 1,
                UserName = model.UserName,
            };

            await _loginRepository.RegisterUser(user, cancellationToken);
        }

    }
}
