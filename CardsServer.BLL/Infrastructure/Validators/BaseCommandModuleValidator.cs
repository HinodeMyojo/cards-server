
using CardsServer.BLL.Dto.Module;
using CardsServer.BLL.Infrastructure.Result;

namespace CardsServer.BLL.Infrastructure.Validators
{
    public class BaseCommandModuleValidator<T> : IValidator<T>
    {
        
        public Result<string> Validate(T obj)
        {
            Type type = typeof(T);
            if (type == typeof(CreateEditModuleBase))
            {
                
            }
        }

        protected static Result<string> CheckTitle(string title)
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
    }
}