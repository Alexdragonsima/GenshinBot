using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using GenshinBot.Data;


namespace GenshinBot.Services
{
    public static class DatabaseInitializer
    {
        public static async Task InitializerAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            // Создаём базу данных, если она не существует
            await context.Database.MigrateAsync();

            // Проверяем, есть ли данные в базе
            if (!context.Elements.Any())
            {
                await SeedDataAsync(context);
            }
        }

        private static async Tasc SeedDataAsync(AppDbContext context)
        {
            // Добавте здесь код для первоначального заполнения базы
            // Вы можете использовать SQL скрипты или С# код

            Console.WriteLine("Seeding database...");

            // Пример добавления элементов
            var pyro = new Models.Database.Element { Name = "Пиро", color = "#FF0000" };
            var hydro = new Models.Database.Element { Name = "Гидро", color = "#0000FF" };
            var electro = new Models.Database.Element { Name = "Электро", color = "#800080" };

            context.Elements.AddRange(pyro,hydro,electro);
            await context.SaveChangesAsync();

            Console.WriteLine("Database seeded successfully!");
        }
    }
}
