using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Dto.Element;
using CardsServer.BLL.Infrastructure.Auth;
using CardsServer.BLL.Infrastructure.Result;
using Microsoft.AspNetCore.Mvc;

namespace CardsServer.API.Controllers
{
    public class ElementController : ControllerBase
    {
        private readonly IElementService _service;

        public ElementController(IElementService service)
        {
            _service = service;
        }

        [HttpPost("element")]
        public async Task<IActionResult> AddElement([FromBody] AddElement model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                int userId = AuthExtension.GetId(User);
                Result result = await _service.AddElement(model, userId, cancellationToken);
                return result.ToActionResult();
            }
            return BadRequest("Модель не прошла проверку!");
        }

        [HttpPut("element")]
        public async Task<IActionResult> EditElement([FromBody] EditElement model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                int userId = AuthExtension.GetId(User);
                Result result = await _service.EditElement(model, userId, cancellationToken);
                return result.ToActionResult();
            }
            return BadRequest("Модель не прошла проверку!");
        }

        /// <summary>
        /// Позволяет получить элемент по его id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("element/{id}")]
        public async Task<IActionResult> GetElementById(int id, CancellationToken cancellationToken)
        {
            int userId = AuthExtension.GetId(User);

            Result<GetElement> result = await _service.GetElementById(id, userId, cancellationToken);

            return result.ToActionResult();
        }


        /// <summary>
        /// Удаляет конкретный элемент
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("element/{id}")]
        public async Task<IActionResult> DeleteElementById(int Id, CancellationToken cancellationToken)
        {
            int userId = AuthExtension.GetId(User);

            Result result = await _service.DeleteElementById(Id, userId, cancellationToken);

            return result.ToActionResult();
        }

        /// <summary>
        /// Удаляет список элементов
        /// </summary>
        /// <param name="Ids"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("element")]
        public async Task<IActionResult> DeleteElements(int[] Ids, CancellationToken cancellationToken)
        {
            int userId = AuthExtension.GetId(User);

            Result result = await _service.DeleteElements(Ids, userId, cancellationToken);

            return result.ToActionResult();
        }
    }
}
