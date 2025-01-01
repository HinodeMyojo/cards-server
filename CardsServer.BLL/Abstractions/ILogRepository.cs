using CardsServer.BLL.Entity;

namespace CardsServer.DAL.Repository
{
    public interface ILogRepository
    {
        void Add(LogsEntity entity, CancellationToken cancellationToken);
    }
}