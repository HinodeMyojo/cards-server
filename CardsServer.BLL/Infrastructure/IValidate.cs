namespace CardsServer.BLL.Infrastructure
{
    public interface IValidate<in T>
    {
        public Result.Result Validate (T entity);
    }
}