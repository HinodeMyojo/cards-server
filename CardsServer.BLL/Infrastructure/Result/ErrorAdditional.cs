using System.Net;

namespace CardsServer.BLL.Infrastructure.Result
{
    public class ErrorAdditional
    {
        public static Error Forbidden = new("Нет доступа", HttpStatusCode.Forbidden);
        public static readonly Error NotFound = new("Не найдено!", HttpStatusCode.NotFound);
        public static readonly Error BadRequest = new("Некорректный запрос", HttpStatusCode.BadRequest);
        public static readonly Error Unauthorized = new("Пользователь не авторизован", HttpStatusCode.Unauthorized);
    }
}
