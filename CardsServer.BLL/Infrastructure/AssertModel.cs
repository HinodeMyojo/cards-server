using CardsServer.BLL.Infrastructure.Result;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CardsServer.BLL.Infrastructure
{
    public class AssertModel
    {
        public static Result<T> CheckNull<T>(T? model) where T : class
        {
            if (model == null)
            {
                return Result<T>.Failure($"Объект {nameof(model)} равен null!");
            }

            return Result<T>.Success(model);
        }
    }
}
