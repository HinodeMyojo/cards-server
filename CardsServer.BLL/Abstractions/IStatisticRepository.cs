using CardsServer.BLL.Entity;
using System.Linq.Expressions;

namespace CardsServer.BLL.Abstractions
{
    public interface IStatisticRepository
    {
        Task<ElementStatisticEntity?> GetElementStatistic(Expression<Func<ElementStatisticEntity, bool>> predicate, CancellationToken cancellationToken);
        Task<ElementStatisticEntity> CreateElementStatistic(UserEntity user, ElementEntity element, CancellationToken cancellationToken);
        Task EditElementStatistic(ElementStatisticEntity elementStatistic, CancellationToken cancellationToken);
    }
}