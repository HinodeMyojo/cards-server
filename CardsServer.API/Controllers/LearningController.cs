using CardsServer.BLL.Abstractions;

using CardsServer.BLL.Infrastructure.Auth;
using CardsServer.BLL.Infrastructure.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CardsServer.API.Controllers
{
    [Authorize]
    [ApiController]
    public sealed class LearningController : ControllerBase
    {
        private readonly ILearningService _service;

        public LearningController(ILearningService service)
        {
            _service = service;
        }

        /// <summary>
        /// Позволяет создать заявку по обучение по определенному модулю (интервал выбирается автоматически).
        /// </summary>
        /// <param name="moduleId">id модуля</param>
        /// <param name="numberOfAttempts">процент верных ответов</param>
        /// <param name="cancellationToken">токен отмены</param>
        /// <returns></returns>
        [HttpPost("learning/repeat-auto")]
        public async Task<IActionResult> CreateLearningManualProcess(
            int moduleId,
            int numberOfAttempts,
            CancellationToken cancellationToken
            )
        {
            int userId = AuthExtension.GetId(User);

            Result result = await _service.CreateLearningManualProcess(userId, moduleId, numberOfAttempts, cancellationToken);

            return result.ToActionResult();
        }

        /// <summary>
        /// Позволяет создать заявку по обучение по определенному модулю (интервал выбирается вручную).
        /// </summary>
        /// <param name="moduleId">id модуля</param>
        /// <param name="cancellationToken">токен отмены</param>
        /// <param name="repeatInterval">интервал повторений</param>
        /// <returns></returns>
        [HttpPost("learning/repeat-manual")]
        public async Task<IActionResult> CreateLearningAutoProcess(
            int moduleId,
            CancellationToken cancellationToken,
            int?[] repeatInterval = null
            )
        {
            int userId = AuthExtension.GetId(User);

            return Ok();
        }
    }
}
