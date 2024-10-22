using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Dto.Element;
using CardsServer.BLL.Dto.Module;
using CardsServer.BLL.Infrastructure.Auth;
using CardsServer.BLL.Infrastructure.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CardsServer.API.Controllers
{
    /// <summary>
    /// Контроллер для работы с модулями
    /// </summary>
    [ApiController]
    //[Authorize]
    public class ModuleController : ControllerBase
    {
        private readonly IModuleService _service;

        public ModuleController(IModuleService service)
        {
            _service = service;
        }

        /// <summary>
        /// Метод создания модулей
        /// </summary>
        /// <param name="module"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("module")]
        public async Task<IActionResult> CreateModule(
            CreateModule module, CancellationToken cancellationToken)
        {
            int userId = AuthExtension.GetId(User);

            Result<int> result = await _service.CreateModule(userId, module, cancellationToken);

            return result.ToActionResult();
        }

        /// <summary>
        /// Метод получения модулей
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("module")]
        public async Task<IActionResult> GetModule(int id, CancellationToken cancellationToken)
        {
            int userId = AuthExtension.GetId(User);

            Result<GetModule> result = await _service.GetModule(userId, id, cancellationToken);

            return result.ToActionResult();
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

        /// <summary>
        /// Добавить модуль в группу "добавленные"
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("add-module")]
        public async Task<IActionResult> AddModule(int moduleId, CancellationToken cancellationToken)
        {
            int userId = AuthExtension.GetId(User);

            Result result = await _service.AddModule(moduleId, userId, cancellationToken);

            return result.ToActionResult();
        }

        /// <summary>
        /// Метод получения добавленнх пользователем модулей
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("used-modules")]
        public async Task<IActionResult> GetUsedModules(CancellationToken cancellationToken)
        {
            int userId = AuthExtension.GetId(User);

            Result<IEnumerable<GetModule>> result = await _service.GetUsedModules(userId, cancellationToken);

            return result.ToActionResult();
        }

        /// <summary>
        /// Метод получения созданных пользователем модулей
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("created-modules")]
        public async Task<IActionResult> GetCreatedModules(CancellationToken cancellationToken)
        {
            int userId = AuthExtension.GetId(User);

            Result<IEnumerable<GetModule>> result = await _service.GetCreatedModules(userId, cancellationToken);

            return result.ToActionResult();
        }
    }
}
