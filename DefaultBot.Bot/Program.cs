
using DefaultBot.Bot.Models.Entities;
using DefaultBot.Bot.Persistance;
using DefaultBot.Bot.Services.BackgroundServices;
using DefaultBot.Bot.Services.Handlers;
using DefaultBot.Bot.Services.UserRepositories;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Polling;

namespace DefaultBot.Bot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Logging.ClearProviders();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddDbContext<BotDbContext>(options =>
            {
                options.UseNpgsql(connectionString: "Host=localhost;Port=5432;Database=TelegramBotDb;User Id=postgres;Password=1352;");
            });
            builder.Services.AddSingleton<IUpdateHandler, BotUpdateHandler>();

            var botConfig = builder.Configuration.GetSection("BotConfiguration")
            .Get<BotConfiguration>()!;

            builder.Services.AddHttpClient("webhook")
                .AddTypedClient<ITelegramBotClient>(httpClient
                    => new TelegramBotClient(botConfig.Token, httpClient));

            builder.Services.AddHostedService<BotBackgroundService>();
            builder.Services.AddHostedService<HelloBackgroundService>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();

            app.UseCors(ops =>
            {
                ops.AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowAnyOrigin();
            });


            app.UseEndpoints(endpoints =>
            {
                var token = botConfig.Token;

                endpoints.MapControllerRoute(
                    name: "webhook",
                    pattern: $"bot/{token}",
                    new { controller = "WebHookConnect", action = "Connector" });

                endpoints.MapControllers();
            });

            app.Run();
        }
    }
}
