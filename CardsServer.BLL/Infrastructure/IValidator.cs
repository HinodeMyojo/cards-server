using CardsServer.BLL.Infrastructure.Result;
using CardsServer.BLL.Infrastructure.Validators;

namespace CardsServer.BLL.Infrastructure
{
    /// <summary>
    /// Общий интерфейс для методов валидации
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IValidator<T>
    {
        public Result<string> Validate (T obj);
    }
}