using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Dto.Profile;
using CardsServer.BLL.Dto.User;
using CardsServer.BLL.Infrastructure.Auth.Enums;
using CardsServer.BLL.Infrastructure.Result;

namespace CardsServer.BLL.Services.Profile;

public class ProfileSerivce : IProfileSerivce
{
    private readonly IUserService _userService;

    public ProfileSerivce(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<Result<GetProfileAccess>> GetAccess(
        string requestedUserName, int userId, CancellationToken cancellationToken)
    {
        // Получение пользователя
        Result<GetBaseUserResponse> userFromService = await _userService.GetByUserName(userId, requestedUserName, cancellationToken);
        Result<GetBaseUserResponse> requestUserResult = await _userService.GetUser(userId, cancellationToken);

        if (!userFromService.IsSuccess)
        {
            return Result<GetProfileAccess>.Failure(userFromService.Error);
        }

        if (!requestUserResult.IsSuccess)
        {
            return Result<GetProfileAccess>.Failure(requestUserResult.Error);
        }
        
        if (userFromService.Value is not GetUserSimpleResponse userFromServerSimple ||
            requestUserResult.Value is not GetUserSimpleResponse requestUserSimple)
        {
            throw new Exception("Невозможно привести модель пользователя к GetUserSimpleResponse!");
        }
        
        GetProfileAccess result = new(userFromServerSimple);
        
        if (userFromServerSimple.Id == userId)
        {
            result.CanViewProfile = true;
            result.IsUserProfile = true;
            result.CanEditUser = true;
            result.CanDeleteUser = true;
        }
        else if (requestUserSimple.RoleId == (int)Role.Admin || requestUserSimple.RoleId == (int)Role.Moderator)
        {
            result.CanViewProfile = true;
            result.CanEditUser = true;
            result.CanDeleteUser = true;
        }
        else if (userFromServerSimple.HasPrivateProfile)
        {
            result.CanViewProfile = false;
        }
        else
        {
            result.CanViewProfile = true;
        }

        return Result<GetProfileAccess>.Success(result);
    }
}