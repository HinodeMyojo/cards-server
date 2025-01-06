using CardsServer.BLL.Dto.User;
using CardsServer.BLL.Infrastructure.Result;
using Microsoft.AspNetCore.JsonPatch;

namespace CardsServer.BLL.Abstractions
{
    public interface IUserService
    {
        Task<Result> EditAvatar(int userId, string newAvatar, CancellationToken cancellationToken);
        Task<Result> EditUser(int id, JsonPatchDocument<PatchUser> patchDoc, CancellationToken cancellationToken);
        Task<Result<GetUserSimpleResponse>> GetByUserName(string userName, int userId, CancellationToken cancellationToken);
        Task<Result<GetUserFullResponse>> GetUser(int userId, CancellationToken cancellationToken);
    }
}