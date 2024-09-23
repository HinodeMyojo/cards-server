using CardsServer.BLL.Infrastructure.RabbitMq;
using Microsoft.AspNetCore.Mvc;

namespace CardsServer.API.Controllers
{
    public class TestController : ControllerBase
    {
        private readonly IRabbitMqService _rabbitMqService;

        public TestController(IRabbitMqService rabbitMqService)
        {
            _rabbitMqService = rabbitMqService;
        }

        [HttpGet("test")]
        public async Task<IActionResult> Test()
        {
            return Ok("Ground Control to Major Tom");
        }

        [HttpGet("test-rabbit")]
        public async Task<IActionResult> TestRabbit()
        {
            _rabbitMqService.SendMessage("Hey");

            return Ok("все ок!");
        }
    }
}
