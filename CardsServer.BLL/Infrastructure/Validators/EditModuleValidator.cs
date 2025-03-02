using CardsServer.BLL.Dto.Module;
using CardsServer.BLL.Infrastructure.Result;

namespace CardsServer.BLL.Infrastructure.Validators
{
    public class EditModuleValidator : BaseCreateEditModuleValidator
    {
        public override Result<string> Validate(object obj)
        {
            try
            {
                Result<string> result = base.Validate(obj);
                if (!result.IsSuccess)
                {
                    return result;
                }
                // TODO
                if (obj is not EditModule module)
                {
                    throw new InvalidCastException("Object is not a EditModule");
                }
                return result;
            }
            catch
            {
                throw;
            }
        }
    }
}