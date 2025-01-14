using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Dto.Card;
using CardsServer.BLL.Dto.Module;
using CardsServer.BLL.Dto.Statistic;
using CardsServer.BLL.Infrastructure.Auth;
using CardsServer.BLL.Infrastructure.Result;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StatisticService.API;


namespace CardsServer.API.Controllers
{
    [ApiController]
    [Authorize]
    public sealed class StatisticController : ControllerBase
    {
        private readonly BLL.Services.gRPC.StatisticService _service;
        private readonly IModuleService _moduleService;

        public StatisticController(
            BLL.Services.gRPC.StatisticService service,
            IModuleService moduleService)
        {
            _service = service;
            _moduleService = moduleService;
        }

        /// <summary>
        /// Проверка соединения с gRPC микросервисом
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("Ping")]
        public async Task<IActionResult> Ping()
        {
            PingResponse result = await _service.PingAsync(new PingRequest());
            return Ok(result);
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

            Timestamp timeNowInTimestampFormat = DateTime.UtcNow.ToTimestamp();

            // Создаем модель для пересылки
            StatisticRequest requestModel = new()
            {
                CompletedAt = timeNowInTimestampFormat,
                UserId = userId,
                ModuleId = moduleStatistic.ModuleId
            };

            // Т.к в protobuf у нас repeated - то наши поля автоматически read-only.
            // То есть инициализировать мы их не можем((
            // Приходится добавлять после инициализации объекта
            if (!moduleStatistic.ElementStatistics.IsNullOrEmpty())
            {
                requestModel.Elements.AddRange(
                    moduleStatistic.ElementStatistics.Select(y => new StatisticElements()
                {
                    Answer = y.Answer,
                    ElementId = y.ElementId
                }));
            }
            StatisticResponse result = await _service
                .SaveStatisticAsync(requestModel);

            return Ok(result);
        }

        [HttpGet("statistic")]
        public async Task<IActionResult> GetStatisticById(int id)
        {
            GetStatisticByIdResponse response = await _service
                .GetStatisticByIdAsync(new GetStatisticByIdRequest { Id = id });

            GetStatisticDto result = new()
            {
                CompletedAt = response.CompletedAt.ToDateTime(),
                NumberOfAttempts = response.NumberOfAttempts,
                PercentSuccess = response.PercentSuccess,
            };

            return Ok(result);
        }


        /// <summary>
        /// Возвращает статистику по действиям юзера в год 
        /// TODO - вынести в отдельный микросервис (gRPC)
        /// </summary>
        /// <returns></returns>
        [HttpGet("statistic/year")]
        public async Task<IActionResult> GetYearStatisic(int userId, int year)
        {
            YearStatisticRequest request = new YearStatisticRequest { UserId = userId, Year = year };
            
            YearStatisticResponse responseFromGrpcService = await _service
                .GetYearStatisicAsync(request);

            YearStatisticDto result = new()
            {
                Year = year,
                MaximumSeries = responseFromGrpcService.MaximumSeries,
                ActiveDays = responseFromGrpcService.ActiveDays,
                NumberOfActions = responseFromGrpcService.NumberOfActions,
                Colspan = [.. responseFromGrpcService.Colspan],
                Data = ConvertRepeatedField(responseFromGrpcService.Data)
            };

            return Ok(result);
        }

        /// <summary>
        /// Позволяет получить список доступных годов статистики
        /// </summary>
        /// <returns></returns>
        [HttpGet("statistic/available-years")]
        public async Task<IActionResult> GetAvailableYears(CancellationToken cancellationToken)
        {
            int userId = AuthExtension.GetId(User);
            return Ok();
        }


        ///// <summary>
        ///// TODO: Позволяет получить все объекты статистики по данному модулю, ассоциированную с пользователем
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet("statistic/{id}")]
        //public async Task<IActionResult> GetAllStatisticForModuleByUser()
        //{
        //    return Ok();
        //}


        /// <summary>
        /// TODO: Получает статистику по определенному модулю
        /// Вероятно надо добавить еще либо дату, либо какой-то уточняющий фактор для получения статистики
        /// </summary>
        /// <returns></returns>
        //[HttpGet("statistic/{id}")]
        //public async Task<IActionResult> GetModuleStatistic(
        //    int id)
        //{
        //    return Ok();
        //}

        ///// <summary>
        ///// Получает статистику по всем модулям, ассоциированым с юзером
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet("statistic")]
        //public async Task<IActionResult> GetModulesStatistic(
        //    )
        //{
        //    return Ok();
        //}

        /// <summary>
        /// Получение данный об активности пользователя
        /// </summary>
        /// <returns></returns>
        [HttpGet("statistic/last-activity")]
        public async Task<IActionResult> GetLastActivity(CancellationToken cancellationToken)
        {
            int userId = AuthExtension.GetId(User);

            GetLastActivityRequest request = new()
            {
                UserId = userId
            };

            GetLastActivityResponse resultLastActivity = await _service
                .GetLastActivityAsync(request);

            LastActivityDTO result;

            if (resultLastActivity.Data.Count == 0)
            {
                result = new();
                return Ok(result);
            }

            int[] moduleIds = resultLastActivity.Data.Select(x => x.ModuleId).ToArray();

            Result<IEnumerable<GetModule>> modules = await _moduleService.GetModulesShortInfo(moduleIds, userId, cancellationToken);

            if (!modules.IsSuccess)
            {
                return BadRequest("Возникла проблема с получением информации по модулям!");
            }

            result = new();

            foreach (GetLastActivityModel? item in resultLastActivity.Data)
            {
                var matchingModule = modules.Value.FirstOrDefault(m => m.Id == item.ModuleId);

                // Исправить в будущем
                if (matchingModule == null)
                {
                    continue;
                }

                result.ActivityList.Add(new()
                {
                    Id = item.ModuleId,
                    AnsweredAt = item.CompletedAt.ToDateTime(),
                    Name = matchingModule.Title
                });
            }

            return Ok(resultLastActivity);
        }

        private static YearStatisticData[][] ConvertRepeatedField(RepeatedField<YearStatisticRow> protobufField)
        {
            return protobufField
                .Select(row => row.Values
                    .Select(value => new YearStatisticData
                    {
                        Date = value.Date.ToDateTime(),
                        Value = value.Value
                    })
                    .ToArray())
                .ToArray();
        }

    }
}
