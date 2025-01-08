using System.Security.Claims;
using CardsServer.BLL.Infrastructure.Auth;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CardsServer.API.Filters;

/// <summary>
/// Фильтр дополнительной авторизации
/// </summary>
public class AccessFilterAttribute :  Attribute, IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        int userId = context.HttpContext.User.GetId();
        
        try
        {
            GetRequestedUserId(context, out int requestedUserId);
        }
        catch (Exception e)
        {
            throw;
        }
    }

    private void GetRequestedUserId(AuthorizationFilterContext context, out int requestedUserId)
    {
        string? requestedUserIdString;
        
        if (context.HttpContext.Request.Method == HttpMethods.Get)
        {
            requestedUserIdString = context.HttpContext.Request.Query["userId"].FirstOrDefault();

            if (requestedUserIdString != null)
            {
                requestedUserId = int.Parse(requestedUserIdString);    
                return;
            }
            else
            {
                throw new Exception("Невозможно получить Id запрашиваемого пользователя");
            }
            
        }
        else
        {
            requestedUserIdString = context.HttpContext.Request.Body["userId"]
        }
    }
}