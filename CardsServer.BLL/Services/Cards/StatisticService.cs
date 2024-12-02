//using CardsServer.BLL.Abstractions;
//using CardsServer.BLL.Dto.Card;
//using CardsServer.BLL.Entity;
//using CardsServer.BLL.Infrastructure.Result;
//using CardsServer.DAL.Repository;

//namespace CardsServer.BLL.Services.Cards
//{
//    public class StatisticService : IStatisticService
//    {
//        private readonly IUserRepository _userRepository;
//        private readonly IModuleRepository _moduleRepository;
//        private readonly IElementRepostory _elementRepostory;

//        public StatisticService(
//            IUserRepository userRepository, 
//            IModuleRepository moduleRepository, 
//            IElementRepostory elementRepostory)
//        {
//            _userRepository = userRepository;
//            _moduleRepository = moduleRepository;
//            _elementRepostory = elementRepostory;
//        }

//        public async Task<Result<IEnumerable<int>>> GetAvailableYears(int userId, CancellationToken cancellationToken)
//        {
//            DateTime timeNow = DateTime.UtcNow;
//            UserEntity? user = await _userRepository.GetUser(userId, cancellationToken);
//            if (user == null)
//            {
//                Result<IEnumerable<int>>.Failure("Не найден пользователь");
//            }
//            DateTime firstUserYear = user!.CreatedAt;
//            ICollection<int> result = [];
//            for (int i = firstUserYear.Year; i <= timeNow.Year; i++)
//            {
//                result.Add(i);
//            }
//            return Result<IEnumerable<int>>.Success(result);
//        }

//        public async Task<Result<GetElementStatistic>> SaveModuleStatistic(
//            int userId, SaveModuleStatistic moduleStatistic, CancellationToken cancellationToken)
//        {
//            UserEntity? user = await _userRepository.GetUser(userId, cancellationToken);
//            //ModuleEntity? module = await _moduleRepository.GetModule(userId, cancellationToken);
//            if (user == null)
//            {
//                return Result<GetElementStatistic>
//                    .Failure("Возникла ошибка при создании модуля. Не удалось идентифицировать пользователя или модуль!");
//            }

//            bool isUsedModule = user.UserModules.Any(x => x.ModuleId == moduleStatistic.ModuleId);

//            GetElementStatistic statistic = new()
//            {
//                ModuleId = moduleStatistic.ModuleId,
//                UserId = userId
//            };

//            ICollection<ElementStatisticEntity> elements = [];

//            foreach (SaveElementStatistic item in moduleStatistic.ElementStatistics)
//            {
//                if (isUsedModule)
//                {
//                    ElementStatisticEntity? elementStatistic;

//                    ElementEntity? element = await _elementRepostory.GetElement(item.ElementId, cancellationToken);
//                    if (element == null)
//                    {
//                        throw new ArgumentException("Element не может быть null!");
//                    }

//                    elementStatistic = await _repository
//                        .GetElementStatistic(x => x.UserId == userId && x.ElementId == item.ElementId, cancellationToken);

//                    if (elementStatistic == null)
//                    {
//                        elementStatistic = await _repository
//                            .CreateElementStatistic(user, element, cancellationToken);
//                    }

//                    elementStatistic.CorrectAnswers += item.Answer ? 1 : 0;
//                    elementStatistic.IncorrectAnswers += item.Answer ? 0 : 1;

//                    elementStatistic.LastAnswer = item.Answer;

//                    await _repository.EditElementStatistic(elementStatistic, cancellationToken);
//                }

//                if (item.Answer)
//                {
//                    statistic.TrueAnswer++;
//                }
//                else
//                {
//                    statistic.FalseAnswer++;
//                }
//            }

//            double percent = ((double)statistic.TrueAnswer / (statistic.TrueAnswer + statistic.FalseAnswer)) * 100;
//            statistic.TrueAnswerPersent = percent;

//            if (isUsedModule)
//            {

//                return Result<GetElementStatistic>.Success(statistic);
//            }

//            return Result<GetElementStatistic>.Success(statistic);

//        }
//    }
//}
