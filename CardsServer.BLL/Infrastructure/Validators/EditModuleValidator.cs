using CardsServer.BLL.Infrastructure.Result;

namespace CardsServer.BLL.Infrastructure.Validators
{
    public class EditModuleValidator : BaseCreateEditModuleValidator
    {
        public override Result<string> Validate(object obj)
        {
            return base.Validate(obj);
        }
    }
}