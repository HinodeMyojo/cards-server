using Microsoft.AspNetCore.Mvc;
namespace CardsServer.BLL.Infrastructure.Result
{
    public static class ResultExtensions
    {
        public static IActionResult ToActionResult<T>(this Result<T> result)
        {
            return result.IsSuccess
                ? new ObjectResult( result.Value ) { StatusCode = result.StatusCode }
                : new ObjectResult( result.Error ) { StatusCode = result.StatusCode };
        }
    }
}
