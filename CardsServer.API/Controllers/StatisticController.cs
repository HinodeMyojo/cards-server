using CardsServer.BLL.Dto.Card;
using CardsServer.BLL.Dto.Statistic;
using CardsServer.BLL.Infrastructure.Auth;
using CardsServer.BLL.Infrastructure.Result;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StatisticService.API;


namespace CardsServer.API.Controllers
{
    [ApiController]
    [Authorize]
    public class StatisticController : ControllerBase
    {
        private readonly BLL.Services.gRPC.StatisticService _service;

        public StatisticController(BLL.Services.gRPC.StatisticService service)
        {
            _service = service;
        }

        /// <summary>
        /// Проверка соединения с gRPC микросервисом
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("Ping")]
        public async Task<IActionResult> Ping()
        {
            PingResponse result;
            try
            {
                result = await _service.PingAsync(new PingRequest());
            }
            catch (Exception ex)
            {
                return BadRequest("Связаться не получилось((");
            }
            return Ok(result);

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

            Timestamp timeNowInTimestampFormat = DateTime.UtcNow.ToTimestamp();

            // Создаем модель для пересылки
            StatisticRequest requestModel = new()
            {
                CompletedAt = timeNowInTimestampFormat,
                UserId = userId,
                ModuleId = moduleStatistic.ModuleId
            };

            // Т.к в protobuf у нас repeated - то наши поля автоматически read-only.
            // То есть инициализировать мы их не можем((
            // Приходится добавлять после инициализации объекта
            if (!moduleStatistic.ElementStatistics.IsNullOrEmpty())
            {
                requestModel.Elements.AddRange(
                    moduleStatistic.ElementStatistics.Select(y => new StatisticElements()
                {
                    Answer = y.Answer,
                    ElementId = y.ElementId
                }));
            }
            StatisticResponse result = await _service
                .SaveStatisticAsync(requestModel);

            return Ok(result);
        }

        [HttpGet("statistic")]
        public async Task<IActionResult> GetStatisticById(int id)
        {
            GetStatisticByIdResponse response = await _service
                .GetStatisticByIdAsync(new GetStatisticByIdRequest { Id = id });
            return Ok(response);
        }


        /// <summary>
        /// Возвращает статистику по действиям юзера в год 
        /// TODO - вынести в отдельный микросервис (gRPC)
        /// </summary>
        /// <returns></returns>
        [HttpGet("statistic/year")]
        public async Task<IActionResult> GetYearStatisic(int year)
        {
            // Пока мокаю
            YearStatisticData[][] res = GenerateYearStatistic(2024);

            List<int> colspan = GenerateColspan(res[6], year);

            YearStatisticDto result = new()
            {
                Year = year,
                Colspan = colspan,
                Data = res
            };

            return Ok(result);

        }

        /// <summary>
        /// Позволяет получить список доступных годов статистики
        /// </summary>
        /// <returns></returns>
        [HttpGet("statistic/available-years")]
        public async Task<IActionResult> GetAvailableYears(CancellationToken cancellationToken)
        {
            int userId = AuthExtension.GetId(User);
            return Ok();
        }


        ///// <summary>
        ///// TODO: Позволяет получить все объекты статистики по данному модулю, ассоциированную с пользователем
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet("statistic/{id}")]
        //public async Task<IActionResult> GetAllStatisticForModuleByUser()
        //{
        //    return Ok();
        //}


        /// <summary>
        /// TODO: Получает статистику по определенному модулю
        /// Вероятно надо добавить еще либо дату, либо какой-то уточняющий фактор для получения статистики
        /// </summary>
        /// <returns></returns>
        //[HttpGet("statistic/{id}")]
        //public async Task<IActionResult> GetModuleStatistic(
        //    int id)
        //{
        //    return Ok();
        //}

        ///// <summary>
        ///// Получает статистику по всем модулям, ассоциированым с юзером
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet("statistic")]
        //public async Task<IActionResult> GetModulesStatistic(
        //    )
        //{
        //    return Ok();
        //}


        private List<int> GenerateColspan(YearStatisticData[] yearStatisticDatas, int year)
        {
            List<int> result = [];
            int count = 0;
            int initialMonth = 1;
            foreach (YearStatisticData item in yearStatisticDatas)
            {
                if (item.Date.Year != year)
                {
                    continue;
                }

                if (item.Date.Month == initialMonth)
                {
                    count++;
                    continue;
                }
                result.Add(count);
                initialMonth++;
                count = 1;
            }

            return result;
        }

        /// <summary>
        /// Получение данный об активности пользователя
        /// </summary>
        /// <returns></returns>
        [HttpGet("statistic/last-activity")]
        public async Task<IActionResult> GetLastActivity()
        {
            int userId = AuthExtension.GetId(User);
            // Пока мокаем
            LastActivityDTO data = new()
            {
                ActivityList = [
                    new LastActivityModel()
                    {
                        Id = 15,
                        Name = "Билибоба"
                    },
                    new LastActivityModel()
                    {
                        Id = 16,
                        Name = "Боба"
                    },
                    new LastActivityModel()
                    {
                        Id = 17,
                        Name = "билиб"
                    }
                    ]
            };

            return Ok(data);
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
