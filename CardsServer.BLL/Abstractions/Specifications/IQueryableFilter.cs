namespace CardsServer.BLL.Abstractions.Specification
{
    // TODO
    public interface IQueryableFilter<T>
        where T: class 
    {
        IQueryable<T> Apply(IQueryable<T> query);
    }
    
    public interface IQueryableOrderBy<T>
    {
        IOrderedQueryable<T> Apply(IQueryable<T> queryable);
    }
    
}