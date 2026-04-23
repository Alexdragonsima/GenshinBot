using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using GenshinBot.Data;
using GenshinBot.Data.Repository;
using GenshinBot.Servicess;
using GenshinBot.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace GenshinBot
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // Инициализация базы данных
            await DatabaseInitializer.InitializerAsync(host.Services);

            // Запуск бота
            var botService = host.Services.GetRequiredService<IBotService>();
            await botService.StartBotAsync();

            await host.RunAsync();
        }
        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    // Конфигурация
                    var configuration = context.Configuration;
                    services.Configure<BotConfiguration>(
                        configuration.GetSection("BotConfiguration"));

                    // База данных
                    services.AddDbContext<AppDbContext>(options =>
                        options.UseSqlServer(
                            configuration.GetConnectionString("DefaultConnection")));

                    // Репозитории
                    services.AddScoped<ICharacterRepository, CharacterRepository>();
                    services.AddScoped<ITeamRepository, TeamRepository>();

                    // Сервисы
                    services.AddScoped<ICharacterService, CharacterService>();
                    services.AddScoped<IBotService, BotService>();

                    // Хостед сервис для бота
                    services.AddHostedService<BotBackgroundService>();
                });
    }

    // Сервис для фонового выполнения бота
    public class BotBackgroundService:BackgroundService
    {
        private readonly IBotService _botService;

        public BotBackgroundService(IBotService botService)
        {
            _botService = botService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _botService.StartBotAsync();
        }
    }
}
