using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Dto.User;
using CardsServer.BLL.Entity;
using CardsServer.BLL.Infrastructure.Result;
using Microsoft.AspNetCore.JsonPatch;

namespace CardsServer.BLL.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result> EditUser(int id, JsonPatchDocument<PatchUser> patchDoc, CancellationToken cancellationToken)
        {


            UserEntity? user = await _repository.GetUser(id, cancellationToken);
            if (user == null)
            {
                return Result<GetUserResponse>.Failure(ErrorAdditional.NotFound);
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

        public async Task<Result<GetUserResponse>> GetUser(int userId, CancellationToken cancellationToken)
        {
            UserEntity? res = await _repository.GetUser(userId, cancellationToken);
            if (res == null)
            {
                return Result<GetUserResponse>.Failure(ErrorAdditional.NotFound);
            }

            GetUserResponse result = new()
            {
                Id = res.Id,
                Email = res.Email,
                StatusId = res.StatusId,
                AvatarId = res.AvatarId,
                UserName = res.UserName,
                IsEmailConfirmed = res.IsEmailConfirmed,
                RoleId = res.RoleId,
            };

            return Result<GetUserResponse>.Success(result);

        }
    }
}
