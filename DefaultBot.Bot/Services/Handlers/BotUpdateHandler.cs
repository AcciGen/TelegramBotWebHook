﻿using DefaultBot.Bot.Models.Entities;
using DefaultBot.Bot.Services.UserRepositories;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace DefaultBot.Bot.Services.Handlers
{
    public class BotUpdateHandler 
    {
        private readonly ITelegramBotClient _botClient;
        private readonly IUserRepository _userRepository;

        public BotUpdateHandler(ITelegramBotClient botClient, IUserRepository userRepository)
        {
            _botClient = botClient;
            _userRepository = userRepository;
        }

        public async Task HandleUpdateAsync(Update update, CancellationToken cancellationToken)
        {
            var message = update.Type switch
            {
                UpdateType.Message => HandleMessageAsync(_botClient, update.Message, cancellationToken),
                _ => HandleRandomMessageAsync(_botClient, update.Message, cancellationToken),
            };

            try
            {
                await message;
            }
            catch
            {
                await message;
            }
        }

        private Task HandleRandomMessageAsync(ITelegramBotClient botClient, Message? message, CancellationToken cancellationToken)
        {
            Console.WriteLine("{0} sent {1} type message", message.From.Username, message?.Type);
            return Task.CompletedTask;
        }

        private async Task HandleMessageAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            try
            {
                var user = new UserModel()
                {
                    Id = message.Chat.Id,
                    Username = message.From.Username
                };

                await _userRepository.AddNewUser(user);

                await botClient.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: $"You said:\n<i>{message.Text}</i>",
                    parseMode: ParseMode.Html,
                    cancellationToken: cancellationToken);
                
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}
