using DefaultBot.Bot.Services.Handlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace DefaultBot.Bot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebHookConnectController : ControllerBase
    {
        private readonly BotUpdateHandler _handler;
        private readonly ITelegramBotClient _client;

        public WebHookConnectController(BotUpdateHandler handler, ITelegramBotClient client)
        {
            _handler = handler;
            _client = client;
        }

        [HttpPost]
        public async Task<IActionResult> Connector([FromBody] Update update, CancellationToken cancellation)
        {
            await _handler.HandleUpdateAsync(_client, update, cancellation);

            return Ok();
        }
    }
}
