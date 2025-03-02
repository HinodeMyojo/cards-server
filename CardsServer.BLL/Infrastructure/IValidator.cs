using CardsServer.BLL.Infrastructure.Result;

namespace CardsServer.BLL.Infrastructure
{
    /// <summary>
    /// Базовый интерфейс для валидатора
    /// </summary>
    public interface IValidator
    {
        public Result<string> Validate (object obj);
    }
}