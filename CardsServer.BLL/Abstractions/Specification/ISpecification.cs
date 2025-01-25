namespace CardsServer.BLL.Abstractions.Specification
{
    /// <summary>
    /// Базовый интерфейс спецификации
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISpecification<T>
    {
        bool IsSatisfiedBy(T candidate);
    }
}