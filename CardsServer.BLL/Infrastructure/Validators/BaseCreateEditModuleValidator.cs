using CardsServer.BLL.Dto.Module;
using CardsServer.BLL.Infrastructure.Result;

namespace CardsServer.BLL.Infrastructure.Validators
{
    /// <summary>
    /// Базовый метод для валидации
    /// </summary>
    public class BaseCreateEditModuleValidator : IValidator
    {
        public virtual Result<string> Validate(object obj)
        {
            try
            {
                if (obj is not CreateEditModuleBase module)
                {
                    throw new InvalidCastException("Object is not a CreateEditModuleBase");
                }
                
                Result<string> resultFromCheckTitle = CheckTitle(module.Title);
                (bool, Result<string>?) check = ResultHandler(resultFromCheckTitle);
                return check.Item1 ? Result<string>.Success() : Result<string>.Failure(check.Item2!.Error);
            }
            catch
            {
                throw;
            }
        }
        private static Result<string> CheckTitle(string title)
        {
            int MIN_LENGTH_OF_MODULE_TITLE = 2;
            int MAX_LENGTH_OF_MODULE_TITLE = 256;

            if (string.IsNullOrWhiteSpace(title))
            {
                return Result<string>.Failure("Название не должно быть пустым или состоять только из пробелов.");
            }

            if (title.Length < MIN_LENGTH_OF_MODULE_TITLE)
            {
                return Result<string>.Failure($"Название должно содержать не менее {MIN_LENGTH_OF_MODULE_TITLE} символов.");
            }

            if (title.Length > MAX_LENGTH_OF_MODULE_TITLE)
            {
                return Result<string>.Failure($"Название должно содержать не более {MAX_LENGTH_OF_MODULE_TITLE} символов.");
            }

            return Result<string>.Success();
        }

        private static (bool, Result<string>?) ResultHandler(params Result<string>[] results)
        {
            foreach (Result<string> item in results)
            {
                if (!item.IsSuccess)
                {
                    return (false, item);
                }
            }
            return (true, null);
        }
    }
}