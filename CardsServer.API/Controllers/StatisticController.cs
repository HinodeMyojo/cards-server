
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

        public static YearStatisticData[][] GenerateYearStatistic(int year)
        {
            YearStatistic pre = new()
            {
                Year = year
            };
            Random rnd = new Random();

            int ab = 30;

            for (int month = 1; month <= 12; month++)
            {
                if (month == 2)
                {
                    ab = 28;
                }
                else
                {
                    ab = 30;
                }
                for (int i = 1; i <= ab; i++)
                {
                    var data = new YearStatisticData()
                    {
                        Date = new DateTime(year, month, i),
                        Value = rnd.Next(0, 3)
                    };
                    pre.Data.Add(data);
                }
               
            }

            YearStatisticData[][] result = new YearStatisticData[7][];

            for (int i = 0; i < 7; i++)
            {
                result[i] = pre.Data.Where(x => (int)x.Date.DayOfWeek == i).ToArray();
            }

            return result;
        }
    }
}
