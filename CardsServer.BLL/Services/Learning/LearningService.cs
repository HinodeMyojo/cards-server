using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Infrastructure.Result;

namespace CardsServer.BLL.Services.Learning
{
    public class LearningService : ILearningService
    {
        private readonly ILearningRepository _repository;

        public LearningService(ILearningRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result> CreateLearningManualProcess(
        int userId,
        int moduleId,
        int numberOfAttempts,
        CancellationToken cancellationToken)
        {
            ICollection<int> learningIntervalInHours;

            try
            {
                learningIntervalInHours = GetLearningInterval(numberOfAttempts);
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }

            // TODO


            return Result.Success("Процесс обучениия запущен");
        }

        private static ICollection<int> GetLearningInterval(int numberOfAttempts)
        {
            const int MATERIAL_IS_UNKNOWN = 6;
            const int LOW_LEVEL_IN_MATERIAL = 5;
            const int MIDDLE_LEVEL_IN_MATERIAL = 4;
            const int HIGHT_LEVEL_IN_MATERIAL = 3;
            const int EXCELENT_LEVEL_IN_MATERIAL = 2;
            const int MATERIAL_IS_LEARNED = 1;

            ICollection<int> learningIntervalInHours;

            if (numberOfAttempts >= MATERIAL_IS_UNKNOWN)
            {
                learningIntervalInHours = [ 1, 3, 12, 24, 48 ];
            }
            else if (numberOfAttempts <= LOW_LEVEL_IN_MATERIAL)
            {
                learningIntervalInHours = [ 3, 6, 12, 24 ];
            }
            else if (numberOfAttempts <= MIDDLE_LEVEL_IN_MATERIAL)
            {
                learningIntervalInHours = [ 6, 12, 24, 48 ];
            }
            else if (numberOfAttempts <= HIGHT_LEVEL_IN_MATERIAL)
            {
                learningIntervalInHours = [ 12, 24, 48, 72 ];
            }
            else if (numberOfAttempts <= EXCELENT_LEVEL_IN_MATERIAL)
            {
                learningIntervalInHours = [24, 48, 72, 168];
            }
            else if (numberOfAttempts == MATERIAL_IS_LEARNED)
            {
                learningIntervalInHours = [ 48 ];
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(numberOfAttempts), "Количество попыток должно быть больше 1 и не быть отрицательным числом!");
            }

            return learningIntervalInHours;
        }
    }
}
