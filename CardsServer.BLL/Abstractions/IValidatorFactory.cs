using CardsServer.BLL.Enums;
using CardsServer.BLL.Infrastructure;

namespace CardsServer.BLL.Abstractions
{
    public interface IValidatorFactory
    {
        public IValidator CreateValidator(ValidateModesEnum validateModes);
    }
}
