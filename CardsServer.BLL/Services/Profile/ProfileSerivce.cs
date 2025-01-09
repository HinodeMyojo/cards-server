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

    public async Task<Result<GetProfileSimpleAccess>> GetAccess(
        string requestedUserName, int userId, CancellationToken cancellationToken)
    {
        GetProfileSimpleAccess result;
        
        Result<GetUserSimpleResponse> userFromService = await _userService.GetByUserName(requestedUserName, cancellationToken);

        if (!userFromService.IsSuccess)
        {
            return Result<GetProfileSimpleAccess>.Failure(userFromService.Error);
        }

        GetUserSimpleResponse user = userFromService.Value;

        // Если запросивший пользователь - хозяин профиля
        if (user.Id == userId)
        {
            result = (GetProfileSimpleAccess) user;
            result.CanViewProfile = true;
            result.IsUserProfile = true;
            result.CanEditUser = true;
            result.CanDeleteUser = true;
            return Result<GetProfileSimpleAccess>.Success(result);
        }

        //else if(user.Id != userId && user.RoleId != Role.Admin)
        //{

        //}
        result = new GetProfileSimpleAccess()
        {
            Avatar = "",
            UserName = "",
        };

        return Result<GetProfileSimpleAccess>.Success(result);
        
    }
    
}