﻿using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Dto;
using CardsServer.BLL.Dto.Element;
using CardsServer.BLL.Dto.Module;
using CardsServer.BLL.Enums;
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
    [Authorize]
    public sealed class ModuleController : ControllerBase
    {
        private readonly IModuleService _service;

        public ModuleController(IModuleService service)
        {
            _service = service;
        }

        /// <summary>
        /// Возвращает настройки таблицы
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("module/header")]
        public IActionResult GetHeaders(CancellationToken cancellationToken)
        {
            ICollection<HeaderDto> results = [
                new HeaderDto{Title="Ключ", Sortable=true, Key="key"},
                new HeaderDto{Title="Значение", Sortable=true, Key="value"},
                new HeaderDto{Title="Контент", Sortable=true, Key="content"},
                ];

            return Ok(results);
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
            int userId = User.GetId();

            module.CreateAt = DateTime.Now.ToUniversalTime();
            
            Result<int> result = await _service.CreateModule(userId, module, cancellationToken);

            return result.ToActionResult();
        }

        [HttpGet("modules/sortOptions")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSortOptions(CancellationToken cancellationToken)
        {
            Dictionary<int, string> sortTime = new()
            {
                { (int)SortTimeEnum.Day, "За день" },
                { (int)SortTimeEnum.Week, "За неделю" },
                { (int)SortTimeEnum.Month, "За месяц" },
                { (int)SortTimeEnum.HalfAYear, "За полгода" },
                { (int)SortTimeEnum.Year, "За год" },
                { (int)SortTimeEnum.AllTime, "За все время" }
            };
            
            Dictionary<int, string> sortOption = new()
            {
                { (int)SortOptionEnum.Newest, "Новые" },
                { (int)SortOptionEnum.Oldest, "Старые" },
                { (int)SortOptionEnum.Popularity, "Популярные" }
            };

            return Ok(new
            {
                sortTime,
                sortOption
            });
        }

        /// <summary>
        /// Метод получения списка модулей
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("modules")]
        public async Task<IActionResult> GetModules([FromQuery]GetModulesRequest request ,CancellationToken cancellationToken)
        {
            // TODO нужно выделить пользователей в отдельный блок, а модули - в другой (чтобы не дублировать image)
            Result<IEnumerable<GetModuleBase>> result = await _service.GetModules(request, cancellationToken);
            
            return result.ToActionResult();    
        }

        /// <summary>
        /// Метод получения модулей
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("module")]
        public async Task<IActionResult> GetModule(int id, CancellationToken cancellationToken)
        {
            int userId = User.GetId();

            Result<GetModule> result = await _service.GetModule(userId, id, cancellationToken);

            return result.ToActionResult();
        }

        /// <summary>
        /// Метод редактирования модулей => TODO
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut("module")]
        public async Task<IActionResult> EditModule(EditModule module, CancellationToken cancellationToken)
        {
            int userId = User.GetId();
            Result result = await _service.EditModule(module, userId, cancellationToken);
            return result.ToActionResult();
        }

        /// <summary>
        /// Метод удаления модулей
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("module")]
        public async Task<IActionResult> DeleteModule(int Id, CancellationToken cancellationToken)
        {
            int userId = User.GetId();

            Result result = await _service.DeleteModule(userId, Id, cancellationToken);

            return result.ToActionResult();
        }

        /// <summary>
        /// Добавить модуль в группу "добавленные"
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("module/used-modules")]
        public async Task<IActionResult> AddModuleToUsed([FromBody]int id, CancellationToken cancellationToken)
        {
            int userId = User.GetId();

            Result result = await _service.AddModuleToUsed(id, userId, cancellationToken);

            return result.ToActionResult();
        }

        /// <summary>
        /// Метод получения добавленных пользователем модулей
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("module/used-modules")]
        public async Task<IActionResult> GetUsedModules(string? textSearch, CancellationToken cancellationToken)
        {
            int userId = User.GetId();

            Result<IEnumerable<GetModule>> result = await _service.GetUsedModules(userId, textSearch, cancellationToken);

            return result.ToActionResult();
        }


        /// <summary>
        /// Метод получения добавленных пользователем модулей (короткий)
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("module/used-modules-short")]
        public async Task<IActionResult> GetUsedShortModules(CancellationToken cancellationToken)
        {
            int userId = User.GetId();

            Result<IEnumerable<GetModule>> result = await _service.GetUsedModules(userId, null ,cancellationToken);

            ICollection<object> shortResult = [];

            foreach (GetModule item in result.Value)
            {
                shortResult.Add(new
                {
                    moduleId = item.Id,
                    title = item.Title,
                    description = item.Description,
                    creatorId = item.CreatorId,
                    elements = getElements(item.Elements)
                });
            }

            var resultAfterProcessing = Result<ICollection<object>>.Success(shortResult);

            return resultAfterProcessing.ToActionResult();
        }


        /// <summary>
        /// Метод получения созданных пользователем модулей
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("module/created-modules")]
        public async Task<IActionResult> GetCreatedModules(CancellationToken cancellationToken)
        {
            int userId = User.GetId();

            Result<IEnumerable<GetModule>> result = await _service.GetCreatedModules(userId, cancellationToken);

            return result.ToActionResult();
        }

        private ICollection<object> getElements(List<GetElement> elements)
        {
            ICollection<object> objects = [];
            foreach (GetElement element in elements)
            {
                objects.Add(new
                {
                    elementId = element.Id,
                    key = element.Key,
                    value = element.Value
                });
            }

            return objects;
        }
    }
}
