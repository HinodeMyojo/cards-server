using CardsServer.BLL.Infrastructure.Result;

namespace CardsServer.BLL.Abstractions
{
    public interface ILearningService
    {
        Task<Result> CreateLearningManualProcess(int userId, int moduleId, int numberOfAttempts, CancellationToken cancellationToken);
    }
}
