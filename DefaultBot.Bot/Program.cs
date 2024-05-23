
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
            builder.Services.AddSingleton(provider => new TelegramBotClient("6907018906:AAGC8-0Z8ePyREKkuZL2nxN0Rei75krJF-I"));

            builder.Services.AddSingleton<IUpdateHandler, BotUpdateHandler>();

            builder.Services.AddHostedService<BotBackgroundService>();
            builder.Services.AddHostedService<HelloBackgroundService>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
