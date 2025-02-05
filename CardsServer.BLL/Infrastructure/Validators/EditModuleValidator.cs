using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Infrastructure.Result;

namespace CardsServer.BLL.Infrastructure.Validators
{
    public class EditModuleValidator<T> : BaseCreateEditModuleValidator<T> where T : IHasTitle
    {
        public override Result<string> Validate(T obj)
        {
            return base.Validate(obj);
        }
    }
}