using CardsServer.BLL.Dto.Profile;
using CardsServer.BLL.Infrastructure.Result;

namespace CardsServer.BLL.Services.Profile;

public interface IProfileSerivce
{
    Task<Result<GetProfileAccess>> GetAccess(string requestedUserName, int userId, CancellationToken cancellationToken);
}