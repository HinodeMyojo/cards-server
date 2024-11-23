
using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Dto.Card;
using CardsServer.BLL.Dto.Statistic;
using CardsServer.BLL.Infrastructure.Auth;
using CardsServer.BLL.Infrastructure.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace CardsServer.API.Controllers
{
    [ApiController]
    [Authorize]
    public class StatisticController : ControllerBase
    {
        private readonly IStatisticService _service;

        public StatisticController(IStatisticService service)
        {
            _service = service;
        }

        /// <summary>
        /// Сохраняет статистику модуля
        /// </summary>
        /// <param name="moduleStatistic"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("statistic")]
        public async Task<IActionResult> SaveModuleStatistic(
            SaveModuleStatistic moduleStatistic, CancellationToken cancellationToken)
        {
            int userId = AuthExtension.GetId(User);

            Result<GetElementStatistic> result = await _service
                .SaveModuleStatistic(userId, moduleStatistic, cancellationToken);

            return result.ToActionResult();
        }

        /// <summary>
        /// TODO: Получает статистику по определенному модулю
        /// </summary>
        /// <returns></returns>
        [HttpGet("statistic/{id}")]
        public async Task<IActionResult> GetModuleStatistic(
            int id)
        {
            return Ok();
        }

        /// <summary>
        /// Получает статистику по всем модулям, ассоциированым с юзером
        /// </summary>
        /// <returns></returns>
        [HttpGet("statistic")]
        public async Task<IActionResult> GetModulesStatistic(
            )
        {
            return Ok();
        }

        /// <summary>
        /// Возвращает статистику по действиям юзера в год 
        /// TODO - вынести в отдельный микросервис (gRPC)
        /// </summary>
        /// <returns></returns>
        [HttpGet("statistic/year")]
        public async Task<IActionResult> GetYearStatisic()
        {
            // Пока мокаю
            YearStatisticData[][] result = GenerateYearStatistic(2024);

            return Ok(result);

        }

        /// <summary>
        /// Генерирует статистику для указанного года.
        /// </summary>
        /// <param name="year">Год, для которого генерируется статистика.</param>
        /// <returns>Двумерный массив, где каждая строка соответствует дням недели.</returns>
        public static YearStatisticData[][] GenerateYearStatistic(int year)
        {
            // Инициализация объекта для хранения данных
            YearStatistic yearStatistic = new()
            {
                Year = year
            };

            Random random = new Random();

            // Проходим по каждому месяцу года
            for (int month = 1; month <= 12; month++)
            {
                // Получаем количество дней в текущем месяце
                int daysInMonth = DateTime.DaysInMonth(year, month);

                for (int day = 1; day <= daysInMonth; day++)
                {
                    var date = new DateTime(year, month, day);

                    // Добавляем данные текущего дня
                    yearStatistic.Data.Add(new YearStatisticData
                    {
                        Date = date,
                        Value = random.Next(0, 3) // Генерация случайного значения
                    });

                    // Для первого дня января добавляем пустые дни до первого дня недели
                    if (month == 1 && day == 1)
                    {
                        int firstDayOfWeek = (int)date.DayOfWeek;

                        // Добавляем пустые дни до первого дня (если не воскресенье)
                        for (int i = 0; i < firstDayOfWeek; i++)
                        {
                            yearStatistic.Data.Insert(0, new YearStatisticData
                            {
                                Date = date.AddDays(-1 - i), // Смещение на предыдущие дни
                                Value = null // Пустое значение для отсутствующих дней
                            });
                        }
                    }
                }
            }

            // Создаем двумерный массив для группировки по дням недели
            YearStatisticData[][] result = new YearStatisticData[7][];

            for (int i = 0; i < 7; i++)
            {
                // Фильтруем данные по конкретному дню недели (0 = Воскресенье, 6 = Суббота)
                result[i] = yearStatistic.Data
                    .Where(x => x.Date.DayOfWeek == (DayOfWeek)i)
                    .ToArray();
            }

            return result;
        }

    }
}
