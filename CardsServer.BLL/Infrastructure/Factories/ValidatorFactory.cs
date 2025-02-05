using CardsServer.BLL.Enums;
using CardsServer.BLL.Infrastructure.CustomExceptions;

namespace CardsServer.BLL.Infrastructure.Factories
{
    public class ValidatorFactory<T> : IValidatorFactory<T>
    {
        public IValidator<T> CreateValidator(ValidateModesEnum validateModes)
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
