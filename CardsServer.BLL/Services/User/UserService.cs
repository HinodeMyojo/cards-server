using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Dto.User;
using CardsServer.BLL.Entity;
using CardsServer.BLL.Infrastructure.Auth.Enums;
using CardsServer.BLL.Infrastructure.Result;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.IdentityModel.Tokens;
using System.Net;

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
             UserEntity? user = await _repository.GetUser(userId, cancellationToken);
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

            UserEntity? user = await _repository.GetUser(id, cancellationToken);
            if (user == null)
            {
                return Result<GetUserFullResponse>.Failure(ErrorAdditional.NotFound);
            }

            var userToPatch = new PatchUser()
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

        public async Task<Result<GetUserSimpleResponse>> GetByUserName(string userName, int userId, CancellationToken cancellationToken)
        {
            // Проверка на пустое имя пользователя
            if (userName.IsNullOrEmpty())
            {
                return Result<GetUserSimpleResponse>.Failure(ErrorAdditional.NotFound);
            }

            // Получение пользователя из репозитория
            UserEntity? user = await _repository.GetUserByUserName(userName, cancellationToken);

            // Проверка, что пользователь найден
            if (user == null)
            {
                return Result<GetUserSimpleResponse>.Failure(ErrorAdditional.NotFound);
            }

            // Проверка, если запрашиваемый пользователь — это тот же самый пользователь
            if (userId == user.Id)
            {
                // Преобразуем пользователя в результат, если это запрос своего профиля
                var result = (GetUserSimpleResponse)user;
                result.IsUserProfile = true;
                return Result<GetUserSimpleResponse>.Success(result);
            }

            // Проверка на блокировку пользователя
            if (user.Status != null && user.Status.Id == (int)Status.Blocked)
            {
                return Result<GetUserSimpleResponse>.Failure(ErrorAdditional.Forbidden);
            }

            // Проверка на приватность профиля
            if (user.IsPrivate)
            {
                return Result<GetUserSimpleResponse>.Failure(ErrorAdditional.Forbidden);
            }

            // Фильтруем только публичные (используемые) модули
            var publicModules = user.UserModules.Where(x => !x.IsPrivateForMe).ToList();
            user.UserModules = publicModules; 

            var userResponse = (GetUserSimpleResponse)user;

            return Result<GetUserSimpleResponse>.Success(userResponse);
        }


        public async Task<Result<GetUserFullResponse>> GetUser(int userId, CancellationToken cancellationToken)
        {
            UserEntity? res = await _repository.GetUser(userId, cancellationToken);
            if (res == null)
            {
                return Result<GetUserFullResponse>.Failure(ErrorAdditional.NotFound);
            }

            GetUserFullResponse result = new()
            {
                Id = res.Id,
                Email = res.Email,
                StatusId = res.StatusId,
                AvatarId = res.AvatarId,
                UserName = res.UserName,
                CreatedAt = res.CreatedAt,
                IsEmailConfirmed = res.IsEmailConfirmed,
                RoleId = res.RoleId,
                Avatar = Convert.ToBase64String(res.Avatar.Data)
            };

            return Result<GetUserFullResponse>.Success(result);

        }
    }
}
