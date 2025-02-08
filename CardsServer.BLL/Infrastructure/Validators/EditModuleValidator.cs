using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Dto.Module;
using CardsServer.BLL.Infrastructure.Result;

namespace CardsServer.BLL.Infrastructure.Validators
{
    public class EditModuleValidator : BaseCreateEditModuleValidator
    {
        public override Result<string> Validate(CreateEditModuleBase obj)
        {
            return base.Validate(obj);
        }
    }
}