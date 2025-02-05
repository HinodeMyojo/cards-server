using CardsServer.BLL.Infrastructure.Result;

namespace CardsServer.BLL.Infrastructure
{
    /// <summary>
    /// Общий интерфейс для методов валидации
    /// </summary>
    public interface IValidator<T>
    {
        public Result<string> Validate (T obj);
    }
}