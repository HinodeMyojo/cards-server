
using CardsServer.BLL.Dto.Card;
using CardsServer.BLL.Infrastructure.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace CardsServer.API.Controllers
{
    [ApiController]
    [Authorize]
    public class CardsController : ControllerBase
    {
        private readonly ICardsService _service;

        public CardsController(ICardsService service)
        {
            _service = service;
        }

        /// <summary>
        /// Сохраняет статистику модуля
        /// </summary>
        /// <param name="moduleStatistic"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("cards/save-module-statistic")]
        public async Task<IActionResult> SaveModuleStatistic(
            SaveModuleStatistic moduleStatistic, CancellationToken cancellationToken)
        {
            int userId = AuthExtension.GetId(User);

            return Ok();
        }
    }
}
