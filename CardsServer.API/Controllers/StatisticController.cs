
using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Dto.Card;
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
        [HttpGet("statistics")]
        public async Task<IActionResult> GetModulesStatistic(
            )
        {
            return Ok();
        }
    }
}
