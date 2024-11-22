
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
            YearStatistic result = GenerateYearStatistic(2024);

            return Ok(result);

        }

        public static YearStatistic GenerateYearStatistic(int year)
        {
            YearStatistic result = new()
            {
                Year = year
            };

            for (int month = 1; month <= 12; month++)
            {
                var monthData = new YearStatisticMonthData
                {
                    Month = month,
                    Data = []
                };

                int daysInMonth = DateTime.DaysInMonth(year, month);
                for (int day = 1; day <= daysInMonth; day++)
                {
                    DateTime date = new DateTime(year, month, day);
                    var dayData = new YearStatisticDayData
                    {
                        Day = day,
                        DayOfWeek = (int)date.DayOfWeek, // День недели (0 - воскресенье, 1 - понедельник, ...)
                        Data = [] // Пустой список данных для дня
                    };
                    monthData.Data.Add(dayData);
                }
                result.Data.Add(monthData);
            }

            

            return result;
        }
    }
}
