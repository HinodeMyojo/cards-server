using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Dto.Element;
using CardsServer.BLL.Infrastructure.Auth;
using CardsServer.BLL.Infrastructure.Result;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;

namespace CardsServer.API.Controllers
{
    public sealed class ElementController : ControllerBase
    {
        private readonly IElementService _service;

        public ElementController(IElementService service)
        {
            _service = service;
        }

        /// <summary>
        /// Позволяет сохранить отдельный элемент. Для сохранения - нужно обязательно указать id существующего модуля.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Microsoft.AspNetCore.Mvc.HttpPost("element")]
        public async Task<IActionResult> AddElement([Microsoft.AspNetCore.Mvc.FromBody] AddElementModel model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                int userId = AuthExtension.GetId(User);
                Result result = await _service.AddElement(model, userId, cancellationToken);
                return result.ToActionResult();
            }
            return BadRequest("Модель не прошла проверку!");
        }

        /// <summary>
        /// Позволяет получить список элементов модуля по moduleId
        /// </summary>
        /// <param name="moduleId">Id модуля</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Microsoft.AspNetCore.Mvc.HttpGet("elements/{moduleId:int}")]
        public async Task<IActionResult> GetElementsByModuleId([FromRoute] int moduleId, CancellationToken cancellationToken)
        {
            Result<IEnumerable<GetElement>> elements = await _service.GetElementsByModuleId(moduleId, cancellationToken);
            
            return elements.ToActionResult();
        }
        
        /// <summary>
        /// Позволяет изменить существующий элемент
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Microsoft.AspNetCore.Mvc.HttpPut("element")]
        public async Task<IActionResult> EditElement([Microsoft.AspNetCore.Mvc.FromBody] EditElementModel model, CancellationToken cancellationToken)
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
        [Microsoft.AspNetCore.Mvc.HttpGet("element/{id}")]
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
        [Microsoft.AspNetCore.Mvc.HttpDelete("element/{id}")]
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
        [Microsoft.AspNetCore.Mvc.HttpDelete("element")]
        public async Task<IActionResult> DeleteElements(int[] Ids, CancellationToken cancellationToken)
        {
            int userId = AuthExtension.GetId(User);

            Result result = await _service.DeleteElements(Ids, userId, cancellationToken);

            return result.ToActionResult();
        }
    }
}
