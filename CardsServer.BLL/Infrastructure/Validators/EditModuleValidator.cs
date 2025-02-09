using CardsServer.BLL.Infrastructure.Result;

namespace CardsServer.BLL.Infrastructure.Validators
{
    public class EditModuleValidator : BaseCreateEditModuleValidator
    {
        public override Result<string> Validate(object obj)
        {
            Result<string> result = base.Validate(obj);
            if (!result.IsSuccess)
            {
                return result;
            }

            return Result<string>.Success(result.Value);

        }
    }
}