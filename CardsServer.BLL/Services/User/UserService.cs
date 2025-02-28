using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Dto.Profile;
using CardsServer.BLL.Dto.User;
using CardsServer.BLL.Entity;
using CardsServer.BLL.Infrastructure.Auth.Enums;
using CardsServer.BLL.Infrastructure.Result;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.IdentityModel.Tokens;

namespace CardsServer.BLL.Services.User
{
    public sealed class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result> EditAvatar(int userId, string newAvatar, CancellationToken cancellationToken)
        {
             UserEntity? user = await _repository.GetUser(x => x.Id == userId, cancellationToken);
            if (user == null)
            {
                return Result.Failure("Пользователь не найден!");
            }
            user.Avatar.Data = Convert.FromBase64String(newAvatar);
            await _repository.EditUser(user, cancellationToken);
            return Result.Success();
        }

        public async Task<Result> EditUser(int id, JsonPatchDocument<PatchUser> patchDoc, CancellationToken cancellationToken)
        {

            UserEntity? user = await _repository.GetUser(x => x.Id == id, cancellationToken);
            if (user == null)
            {
                return Result.Failure(ErrorAdditional.NotFound);
            }

            PatchUser userToPatch = new()
            {
                UserName = user.UserName,
                Email = user.Email,
                Avatar = user.Avatar,
            };

            patchDoc.ApplyTo(userToPatch);

            user.UserName = userToPatch.UserName;
            user.Email = userToPatch.Email;

            await _repository.EditUser(user, cancellationToken);

            return Result.Success();
        }

        public async Task<Result<GetBaseUserResponse>> GetByUserName(
            int userId, string userName, CancellationToken cancellationToken)
        {
            // Проверка на пустое имя пользователя
            if (userName.IsNullOrEmpty())
            {
                return Result<GetBaseUserResponse>.Failure("Логин пользователя должен быть указан!");
            }

            // Получение пользователя из репозитория
            UserEntity? user = await _repository.GetUser(x => x.UserName == userName, cancellationToken);

            // Проверка, что пользователь найден
            
            if (user == null)
            {
                return Result<GetBaseUserResponse>.Failure(ErrorAdditional.NotFound);
            }
            GetBaseUserResponse response = await UserAccessHandler(user, userId);

            return Result<GetBaseUserResponse>.Success(response);
        }

        public async Task<Result<GetBaseUserResponse>> GetUser(int userId, CancellationToken cancellationToken)
        {
            UserEntity? user = await _repository.GetUser(x => x.Id == userId, cancellationToken);
            if (user == null)
            {
                return Result<GetBaseUserResponse>.Failure(ErrorAdditional.NotFound);
            }
            
            GetBaseUserResponse response = await UserAccessHandler(user, userId);

            return Result<GetBaseUserResponse>.Success(response);
        }

        public async Task<Result<GetBaseUserResponse>> GetUser(int userId, int userRequestedId, CancellationToken cancellationToken)
        {
            UserEntity? user = await _repository.GetUser(x => x.Id == userId, cancellationToken);
            if (user == null)
            {
                return Result<GetBaseUserResponse>.Failure(ErrorAdditional.NotFound);
            }

            GetBaseUserResponse response = await UserAccessHandler(user, userRequestedId);

            return Result<GetBaseUserResponse>.Success(response);
        }

        /// <summary>
        /// Вспомогательный метод.
        /// Если запросил хозяин профиля (или админ или модер)  - отдаем всю инфу, иначе - только часть
        /// </summary>
        /// <param name="userFromDatabase"></param>
        /// <param name="userIdFromRequest"></param>
        /// <returns></returns>
        private async Task<GetBaseUserResponse> UserAccessHandler(UserEntity userFromDatabase, int userIdFromRequest)
        {
            if (userIdFromRequest == 0)
            {
                return new GetUserSimpleResponse(userFromDatabase);
            }

            UserEntity? requester = await _repository.GetUser(x => x.Id == userIdFromRequest, CancellationToken.None);
            if (requester == null)
            {
                throw new Exception("Запрашивающий пользователь не найден!");
            }

            // Если запрашивающий пользователь администратор или модератор
            if (requester.RoleId is (int)Role.Admin or (int)Role.Moderator)
            {
                return new GetUserFullResponse(userFromDatabase);
            }

            // Если пользователь запрашивает свой профиль
            if (userFromDatabase.Id == userIdFromRequest)
            {
                return new GetUserFullResponse(userFromDatabase);
            }
            return new GetUserSimpleResponse(userFromDatabase);
        }

    }

}