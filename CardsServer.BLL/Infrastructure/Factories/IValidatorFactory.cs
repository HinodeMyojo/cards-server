using CardsServer.BLL.Enums;

namespace CardsServer.BLL.Infrastructure.Factories
{
    public interface IValidatorFactory<T>
    {
        public IValidator<T> CreateValidator (ValidateModesEnum validateModes);
    }
}
