﻿using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Dto.User;
using CardsServer.BLL.Entity;
using CardsServer.BLL.Infrastructure.Auth;
using CardsServer.BLL.Infrastructure.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CardsServer.API.Controllers
{
    [ApiController]
    [Authorize]
    public sealed class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Позволяет получить пользователя по его Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("user/get")]
        public async Task<IActionResult> GetUser(int id, CancellationToken cancellationToken)
        {
           int userRequestedId = User.GetId();

           Result<GetBaseUserResponse> response = await _userService.GetUser(id, userRequestedId,  cancellationToken);

           return response.ToActionResult();
        }

        /// <summary>
        /// Позволяет получить информацию о пользователе передав его UserName
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("user/getByUserName")]
        public async Task<IActionResult> GetByUserName(string userName, CancellationToken cancellationToken)
        {
            int userId = User.GetId();

            Result<GetBaseUserResponse> result = await _userService.GetByUserName(userId, userName, cancellationToken);

            return result.ToActionResult();
        }

        [HttpPut("user/avatar")]
        public async Task<IActionResult> EditAvatar([FromBody]string newAvatar, CancellationToken cancellationToken)
        {
            int userId = User.GetId();

            Result result = await _userService.EditAvatar(userId, newAvatar, cancellationToken);

            return result.ToActionResult();
        }

        /// <summary>
        /// Возвращает информацию о залогинненом пользователе
        /// </summary>
        /// <returns></returns>
        [HttpGet("user/whoami")]
        public async Task<IActionResult> Whoami(CancellationToken cancellationToken)
        {
            int userId = User.GetId();

            Result<GetBaseUserResponse> result = await _userService.GetUser(userId, cancellationToken);

            return result.ToActionResult();
        }

        /// <summary>
        /// <para>Позволяет изменить (пока что) email и username пользователя.</para>
        /// Отправлять запрос в формате:
        /// <code>
        /// [
        ///    { 
        ///        "op": "replace", 
        ///        "path": "/username", 
        ///        "value": "новое_значение"
        ///    },
        ///    { 
        ///        "op": "replace", 
        ///        "path": "/email", 
        ///        "value": "новый_email@example.com"
        ///    }
        /// ]
        /// </code>
        /// <para>Состояние подтверждения email после изменения сохраняется.</para>
        /// </summary>
        /// <param name="patchDoc">Документ с операциями изменения (JSON Patch Document).</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Результат выполнения операции.</returns>

        [HttpPatch("user/edit/{id}")]
        public async Task<IActionResult> EditUser(int id, [FromBody] JsonPatchDocument<PatchUser> patchDoc, CancellationToken cancellationToken)
        {
            int userFromTokenId = User.GetId();

            if (patchDoc == null)
            {
                return BadRequest(new { message = "Patch document cannot be null." });
            }

            // Проверяем, что пользователь редактирует свой профиль
            if (id != userFromTokenId)
            {
                return Forbid();
            }

            Result result = await _userService.EditUser(id, patchDoc, cancellationToken);

            return result.ToActionResult();
        }
    }
}
