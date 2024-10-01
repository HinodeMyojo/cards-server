using CardsServer.BLL.Dto.User;
using CardsServer.BLL.Infrastructure.Result;
using Microsoft.AspNetCore.JsonPatch;

namespace CardsServer.BLL.Abstractions
{
    public interface IUserService
    {
        Task<Result> EditUser(int id, JsonPatchDocument<PatchUser> patchDoc, CancellationToken cancellationToken);
        Task<Result<GetUserResponse>> GetUser(int userId, CancellationToken cancellationToken);
    }
}