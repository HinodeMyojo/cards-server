using CardsServer.BLL;
using CardsServer.BLL.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CardsServer.API.Controllers
{
    public class AnalyticController : ControllerBase
    {
        private readonly IAnalyticService _service;

        public AnalyticController(IAnalyticService service)
        {
            _service = service;
        }

        [HttpGet("test")]
        public async Task<IActionResult> Test()
        {
            AnalyticsResponse res = await _service.SendTestDataAsync(new AnalyticsRequest() { Id = 1 });

            return Ok(res);
        }
    }
}
