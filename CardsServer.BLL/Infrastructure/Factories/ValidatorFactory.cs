﻿using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Enums;
using CardsServer.BLL.Infrastructure.CustomExceptions;
using CardsServer.BLL.Infrastructure.Validators;

namespace CardsServer.BLL.Infrastructure.Factories
{
    public class ValidatorFactory : IValidatorFactory
    {
        IValidator _validator;

        public IValidator CreateValidator(ValidateModesEnum validateModes)
        {
            switch (validateModes)
            {
                case ValidateModesEnum.EditModuleByAdmin:
                    _validator = new EditModuleValidator();
                    break;
                case ValidateModesEnum.EditModuleByUser:
                    _validator = new EditModuleValidator();
                    break;
                case ValidateModesEnum.CreateModuleByUser:
                    _validator = new BaseCreateEditModuleValidator();
                    break;
                default:
                    throw new NotSelectedValidatorException("Не выбран тип валидации!");
            }

            return _validator;
        }
    }
}
