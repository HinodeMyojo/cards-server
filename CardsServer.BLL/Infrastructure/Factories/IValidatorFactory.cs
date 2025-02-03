using CardsServer.BLL.Enums;

namespace CardsServer.BLL.Infrastructure.Factories
{
    public interface IValidatorFactory
    {
        public IValidator CreateValidator (ValidateModesEnum validateModes);
    }
}
