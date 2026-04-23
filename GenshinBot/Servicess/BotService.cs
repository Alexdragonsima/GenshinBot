using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using GenshinBot.Config;
using GenshinBot.Servicess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace GenshinBot.Servicess
{
    public class BotService : IBotService
    {
        private readonly BotConfiguration _botConfig;
        private readonly ICharacterService _characterService;
        private TelegramBotClient _botClient;
        private CancellationTokenSource _cancellationTokenSource;

        public BotService(
            IOptions<BotConfiguration> botConfig,
            ICharacterService characterService)
        {
            _botConfig = botConfig.Value;
            _characterService = characterService;
            _botClient = new TelegramBotClient(_botConfig.BotToken);
        }

        public async Task StartBotAsync()
        {
            _cancellationTokenSource = new CancellationTokenSource();

            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = Array.Empty<UpdateType>()
            };

            _botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                errorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: _cancellationTokenSource.Token
            );

            var me = await _botClient.GetMe();
            Console.WriteLine($"Бот запущен: @{me.Username}");
        }

        public Task StopBotAsync()
        {
            _cancellationTokenSource?.Cancel();
            return Task.CompletedTask;
        }

        private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Message is not { } message)
                return;

            if (message.Text is not { } messageText)
                return;

            var chatId = message.Chat.Id;
            Console.WriteLine($"Получено сообщение от {chatId}: {messageText}");

            // Обработка команд
            var response = messageText switch
            {
                "/start" => await HandleStartCommand(chatId),
                "/help" => await HandleHelpCommand(chatId),
                "/characters" => await HandleCharactersCommand(chatId),
                _ => await HandleCharacterSearch(messageText, chatId)
            };

            await botClient.SendMessage(
                chatId: chatId,
                text: response,
                parseMode: ParseMode.Markdown,
                cancellationToken: cancellationToken);
        }

        private Task<string> HandleStartCommand(long chatId)
        {
            var response = "🕹️ *Добро пожаловать в Genshin Impact Bot!*\n\n" +
                          "Я помогу вам найти лучшие команды для персонажей.\n\n" +
                          "📝 *Доступные команды:*\n" +
                          "/start - Начать работу\n" +
                          "/help - Помощь\n" +
                          "/characters - Список всех персонажей\n\n" +
                          "Также вы можете просто ввести имя персонажа (например, 'Ху Тао')";
            return Task.FromResult(response);
        }

        private Task<string> HandleHelpCommand(long chatId)
        {
            var response = "❓ *Помощь*\n\n" +
                          "Этот бот предоставляет информацию о персонажах Genshin Impact и их лучших командах.\n\n" +
                          "🔍 *Как использовать:*\n" +
                          "1. Введите имя персонажа\n" +
                          "2. Или выберите персонажа из списка командой /characters\n" +
                          "3. Получите информацию о лучших командах для него\n\n" +
                          "📚 *Источники данных:*\n" +
                          "🔹Genshin Impact Wiki\n" +
                          "🔹Сообщество игроков\n" +
                          "🔹Мета-анализ";
            return Task.FromResult(response) ;
        }

        private async Task<string> HandleCharactersCommand(long chatId)
        {
            try
            {
                var characters = await _characterService.GetAllCharactersAsync();

                if (!characters.Any())
                    return "База данных персонажей пуста.";

                var response = new System.Text.StringBuilder();
                response.AppendLine("🤺 *Список персонажей:*\n");

                foreach (var character in characters)
                {
                    response.AppendLine($"🔹 *{character.CharacterName}* - {character.Element?.ElementName} {character.WeaponType?.WeaponTypeName}");
                }

                response.AppendLine("\nВведите имя для получения подробной информации.");

                return response.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении списка персонажей: {ex.Message}");
                return "Произошла ошибка при получении списка персонажей.";
            }
        }

        private async Task<string> HandleCharacterSearch(string messageText, long chatId)
        {
            try
            {
                var characterInfo = await _characterService.GetCharacterInfoAsync(messageText);
                var teamsInfo = await _characterService.GetCharacterTeamsInfoAsync(messageText);

                return characterInfo + "\n\n" + teamsInfo;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Ошибка при поиске персонажа: {ex.Message}");
                return $"Не удалось найти информацию о персонаже '{messageText}'. Попробуйте еще раз.";
            }
        }

        private Task HandlePollingErrorAsync(ITelegramBotClient botClient,Exception exception,CancellationToken cancellationToken)
        {
            var errorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(errorMessage);
            return Task.CompletedTask;
        }
    }
}
