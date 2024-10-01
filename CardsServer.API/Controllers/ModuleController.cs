using Microsoft.AspNetCore.Mvc;

namespace CardsServer.API.Controllers
{
    /// <summary>
    /// Контроллер для работы с модулями
    /// </summary>
    public class ModuleController : ControllerBase
    {
        [HttpPost("module")]
        public async Task<IActionResult> CreateModule(CancellationToken cancellationToken)
        {
            return Ok();
        }

        [HttpGet("module")]
        public async Task<IActionResult> GetModule(CancellationToken cancellationToken)
        {
            return Ok();
        }

        [HttpPut("module")]
        public async Task<IActionResult> EditModule(CancellationToken cancellationToken)
        {
            return Ok();
        }

        [HttpDelete("module")]
        public async Task<IActionResult> DeleteModule(CancellationToken cancellationToken)
        {
            return Ok();
        }
    }
}
