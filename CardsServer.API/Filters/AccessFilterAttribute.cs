using Microsoft.AspNetCore.Mvc.Filters;

namespace CardsServer.API.Filters;

/// <summary>
/// Фильтр дополнительной авторизации
/// </summary>
public class AccessFilterAttribute :  Attribute, IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var bfgrg = context;
        Console.WriteLine(",jfg");
    }
}