using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Dto;
using CardsServer.BLL.Dto.Login;
using CardsServer.BLL.Entity;
using CardsServer.BLL.Infrastructure;
using CardsServer.BLL.Infrastructure.Result;

namespace CardsServer.BLL.Services.User
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        private readonly IUserRepository _userRepository; 
        private readonly IJwtGenerator _jwtGenerator;

        public LoginService(ILoginRepository loginRepository, IJwtGenerator jwtGenerator, IUserRepository userRepository)
        {
            _loginRepository = loginRepository;
            _jwtGenerator = jwtGenerator;
            _userRepository = userRepository;
        }

        public async Task<Result<string>> LoginUser(LoginUser user, CancellationToken cancellationToken)
        {

            UserEntity? res = await _loginRepository.GetUser(user, cancellationToken);
            Result<UserEntity> userResult = AssertModel.CheckNull(res);

            if (!userResult.IsSuccess)
            {
                return Result<string>.Failure(userResult.Error, userResult.StatusCode);
            }

            if (!PasswordExtension.CheckPassword(res.Password, user.Password))
            {
                return Result<string>.Failure("Пароли не сопадают.");
            }

            string token = _jwtGenerator.GenerateToken(res);
            return Result<string>.Success(token);
        }
        public async Task<Result> RegisterUser(RegisterUser model, CancellationToken cancellationToken)
        {
            if (await IsEmailUsedAsync(model.Email))
            {
                return Result.Failure("Пользователь с таким Email уже зарегистрирован!");
            }

            UserEntity user = new()
            {
                Email = model.Email,
                Password = PasswordExtension.HashPassword(model.Password),
                IsEmailConfirmed = false,
                RoleId = 1,
                StatusId = 1,
                UserName = model.UserName,
            };

            await _loginRepository.RegisterUser(user, cancellationToken);

            return Result.Success();
        }

        private async Task<bool> IsEmailUsedAsync(string email)
        {
            UserEntity? user = await _userRepository.GetUserByEmail(email, default);
            return user != null;
        }

    }
}
