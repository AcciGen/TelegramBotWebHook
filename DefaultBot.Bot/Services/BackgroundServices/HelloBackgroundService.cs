﻿using DefaultBot.Bot.Models.Entities;
using DefaultBot.Bot.Services.UserRepositories;
using Telegram.Bot;

namespace DefaultBot.Bot.Services.BackgroundServices
{
    public class HelloBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ITelegramBotClient _client;

        public HelloBackgroundService(IServiceScopeFactory serviceScopeFactory, ITelegramBotClient client)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _client = client;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
                    var users = await userRepository.GetAllUsers();

                    foreach (var user in users)
                    {
                        await SendNotification(user, stoppingToken);
                    }
                }
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }

        private Task SendNotification(UserModel user, CancellationToken token)
        {
            try
            {
                return _client.SendTextMessageAsync(
                    chatId: user.Id,
                    text: "What's up, my nigga?",
                    cancellationToken: token);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Someone blocked me ~_~");
                return Task.CompletedTask;
            }
        }
    }
}
