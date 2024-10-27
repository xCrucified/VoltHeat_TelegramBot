using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace VoltHeat_TelegramBot
{
    public class Host
    {
        public Action<ITelegramBotClient, Update>? OnMessage;
        private TelegramBotClient _bot;

        public ReplyKeyboardMarkup replyKeyboard {  get; set; }
        public ReplyKeyboardMarkup replyKeyboard2 {  get; set; }

        public Host(string token)
        {
            _bot = new TelegramBotClient(token);

            replyKeyboard = new ReplyKeyboardMarkup(new[]
            {
            new KeyboardButton[] { "\u26A1 Типи електрокотлів", "\U0001F4D9 Контактна інформація"},
            new KeyboardButton[] { "\U00002139\U0000FE0F  Інформація про бота", "\U0001F916 Чат з АІ" }
            })
            {
                ResizeKeyboard = true
            };

            replyKeyboard2 = new ReplyKeyboardMarkup(new[]
            {
                new KeyboardButton[] { "ТЕНовий котел", "Індукційний котел", "Електродний котел", "\u25C0\uFE0F Назад" }
            })
            {
                ResizeKeyboard = true
            };
        }

        public void Start()
        {
            _bot.StartReceiving(UpdateHandler, ErrorHandler);
            Console.WriteLine("Bot is runnin'");
        }

        private async Task ErrorHandler(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            Console.WriteLine("Error: " + exception.Message);
            await Task.CompletedTask;
        }

        private async Task UpdateHandler(ITelegramBotClient client, Update update, CancellationToken token)
        {
            Console.WriteLine($"Received message: {update.Message?.Text}");
            OnMessage?.Invoke(client, update);
            await Task.CompletedTask;
        }
    }
}
