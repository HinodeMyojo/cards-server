using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace CardsServer.BLL.Infrastructure.Auth;

public static class AuthExtension
{
    public static int GetId(ClaimsPrincipal user)
    {
        string? userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null && userId == "0")
        {
            throw new Exception("Не удалось получить пользователя из токена");
        }
        return Convert.ToInt32(userId);
    }
}
