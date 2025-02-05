using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Infrastructure.Result;
using System.ComponentModel.DataAnnotations;

namespace CardsServer.BLL.Infrastructure.Validators
{
    /// <summary>
    /// Базовый валидатор для методов Create, Edit Модулей
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseCreateEditModuleValidator<T> : IValidator<T> where T : IHasTitle
    {
        
        public virtual Result<string> Validate(T obj)
        {
            try
            {
                Result<string> resultFromCheckTitle = CheckTitle(obj.Title);
                var check = ResultHandler(resultFromCheckTitle);
                if (check.Item1)
                {
                    return Result<string>.Success();
                }
                else
                {
                    return Result<string>.Failure(check.Item2!.Error);
                }
            }
            catch(Exception ex)
            {
                throw new ValidationException("Возникла ошибка при валидации", ex);
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

        public static (bool, Result<string>?) ResultHandler(params Result<string>[] results)
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