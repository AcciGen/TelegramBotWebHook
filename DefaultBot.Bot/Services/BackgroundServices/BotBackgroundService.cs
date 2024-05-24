using Telegram.Bot.Polling;
using Telegram.Bot;
using DefaultBot.Bot.Services.UserRepositories;
using Telegram.Bot.Types.Enums;
using DefaultBot.Bot.Models.Entities;

namespace DefaultBot.Bot.Services.BackgroundServices
{
    public class BotBackgroundService : BackgroundService
    {
        private readonly BotConfiguration _configuration;
        private readonly ITelegramBotClient _botClient;

        public BotBackgroundService(IConfiguration configuration, ITelegramBotClient botClient)
        {
            _configuration = configuration.GetSection("BotConfiguration").Get<BotConfiguration>()!;
            _botClient = botClient;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var webhookAddress = $"{_configuration.HostAddress}/bot/{_configuration.Token}";

            await _botClient.SendTextMessageAsync(
                chatId: _configuration.MyChatId,
                text: "Start weebhook");

            await _botClient.SetWebhookAsync(
                url: webhookAddress,
                allowedUpdates: Array.Empty<UpdateType>(),
                cancellationToken: stoppingToken);
        }
    }
}
