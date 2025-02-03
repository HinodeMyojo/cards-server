using CardsServer.BLL.Enums;
using CardsServer.BLL.Infrastructure.CustomExceptions;
using CardsServer.BLL.Infrastructure.Validators;

namespace CardsServer.BLL.Infrastructure.Factories
{
    public class ValidatorFactory : IValidatorFactory
    {
        public IValidator<T> CreateValidator<T>(ValidateModesEnum validateModes) where T : class
        {
            IValidator<T> validator = null;

            switch (validateModes)
            {
                case ValidateModesEnum.EditModuleByUser:
                    validator = new EditModuleValidator<T>();
                    break;
                default:
                    throw new NotSelectedValidatorException("Не выбран тип валидации!");
            }

            return validator;
        }
    }
}
