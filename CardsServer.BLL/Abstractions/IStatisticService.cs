using CardsServer.BLL.Dto.Card;
using CardsServer.BLL.Infrastructure.Result;

namespace CardsServer.BLL.Abstractions
{
    public interface IStatisticService
    {
        Task<Result<GetElementStatistic>> SaveModuleStatistic(int userId, SaveModuleStatistic moduleStatistic, CancellationToken cancellationToken);
    }
}