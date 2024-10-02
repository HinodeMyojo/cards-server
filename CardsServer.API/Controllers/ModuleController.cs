using Microsoft.AspNetCore.Mvc;

namespace CardsServer.API.Controllers
{
    /// <summary>
    /// Контроллер для работы с модулями
    /// </summary>
    public class ModuleController : ControllerBase
    {
        /// <summary>
        /// Метод создания модулей
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("module")]
        public async Task<IActionResult> CreateModule(CancellationToken cancellationToken)
        {
            return Ok();
        }

        /// <summary>
        /// Метод получения модулей
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("module")]
        public async Task<IActionResult> GetModule(CancellationToken cancellationToken)
        {
            return Ok();
        }

        /// <summary>
        /// Метод редактирования модулей
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut("module")]
        public async Task<IActionResult> EditModule(CancellationToken cancellationToken)
        {
            return Ok();
        }

        /// <summary>
        /// Метод удаления модулей
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("module")]
        public async Task<IActionResult> DeleteModule(CancellationToken cancellationToken)
        {
            return Ok();
        }
    }
}
