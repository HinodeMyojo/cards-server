using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CardsServer.BLL.Infrastructure.Auth;

public static class AuthExtension
{
    public static int GetId(this ClaimsPrincipal user)
    {
        string? userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        // Проверка на null или значение "0"
        if (string.IsNullOrEmpty(userId) || userId == "0")
        {
            throw new Exception("Не удалось получить пользователя из токена");
        }

        return Convert.ToInt32(userId);
    }
}
