using CardsServer.BLL.Entity;

namespace CardsServer.DAL.Repository
{
    public interface ILogRepository
    {
        Task Add(LogsEntity entity, CancellationToken cancellationToken);
    }
}