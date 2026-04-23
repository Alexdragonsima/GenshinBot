using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GenshinBot.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenshinBot
{
    public static class DatabaseTest
    {
        public static async Task TestConnectionAsync()
        {
            try
            {
                // Настройка
                var configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();

                var services = new ServiceCollection();
                services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

                var serviceProvider = services.BuildServiceProvider();
                var context = serviceProvider.GetRequiredService<AppDbContext>();

                // Проверка подключения
                Console.WriteLine("Проверка подключения к базе данных...");
                var canConnect = await context.Database.CanConnectAsync();

                if (canConnect)
                {
                    Console.WriteLine("✔️ Подключение к базе данных успешно!");

                    // Проверка наличия таблиц
                    var tableExist = await context.Characters.AnyAsync();
                    Console.WriteLine(tableExist ? "✔️ Таблицы созданы и содержат данные" : "⚠️ Таблицы пусты");
                }
                else
                {
                    Console.WriteLine("❌ Не удалось подключиться к базе данных");
                }    
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Ошибка при тестировании подключения: {ex.Message}");
            }
        }
    }
}
