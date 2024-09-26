using Microsoft.AspNetCore.Mvc;
namespace CardsServer.BLL.Infrastructure.Result
{
    public static class ResultExtensions
    {
        public static IActionResult ToActionResult(this Result result, string? message=null)
        {
            return result.IsSuccess
                ? new ObjectResult(message ?? "Все работает") { StatusCode = result.StatusCode }
                : new ObjectResult( result.Error ) { StatusCode = result.StatusCode };
        }

        public static IActionResult ToActionResult<T>(this Result<T> result)
        {
            return result.IsSuccess
                ? new ObjectResult(result.Value) { StatusCode = result.StatusCode }
                : new ObjectResult(result.Error) { StatusCode = result.StatusCode };
        }
    }
}
