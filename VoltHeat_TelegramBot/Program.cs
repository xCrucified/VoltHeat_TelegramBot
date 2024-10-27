using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using VoltHeat_TelegramBot;
using static System.Net.Mime.MediaTypeNames;

public class Program
{
    public static string token { get; set; } = "7580066425:AAHM1S9ybfK4t_ZxYc-wky0vtLuJEXWQOQU";
    public static Host telegramBot = new(token);

    private async Task HandleCallbackQueryAsync(ITelegramBotClient client, CallbackQuery callbackQuery)
    {
        //if (callbackQuery.Data == "go_back")
        //{
        //    await SelectOption(client, callbackQuery.);
        //}
    }

    private static async void SelectOption(ITelegramBotClient client, Update update)
    {
        switch (update.Message?.Text)
        {
            case "ТЕНовий котел":
                string tmp = "C:\\Users\\PC\\source\repos\\VoltHeat_TelegramBot\\VoltHeat_TelegramBot\\Images";
                string text = "ТЕНовий котел опалення є одним із найпоширеніших як у побутовому, так і в промисловому використанні.\r\nПереваги:\r\n1) Простота і надійність: проста конструкція ТЕНових котлів забезпечує їх надійність і зручність в обслуговуванні. Вони ідеально підходять для домовласників, які віддають перевагу простим і зрозумілим технологіям.\r\n2) Економія: ці котли часто дешевші під час купівлі та встановлення, порівняно з іншими видами електрокотлів.\r\nНедоліки:\r\n1) Утворення накипу: в регіонах з жорсткою водою ТЕНи можуть швидко заростати накипом, що вимагає їх регулярного чищення або заміни.\r\n2) Чутливість до напруги: у більшості випадків знадобляться запобіжник від перепадів напруги в мережі.";
                using (var stream = new FileStream(tmp, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    await client.SendPhotoAsync(
                        chatId: update.Message.Chat,
                        photo: new InputFileStream(stream, Path.GetFileName(tmp)),
                        caption: text
                    );
                }
                break;
            case "Індукційний котел":
                break;
            case "Електродний котел":
                break;
            case "\u25C0\uFE0F Назад":
                await client.SendTextMessageAsync(
                chatId: update.Message.Chat,
                text: "Виберіть пункт",
                replyMarkup: telegramBot.replyKeyboard
                );
                break;
        }
    }


    private static async void OnMessage(ITelegramBotClient client, Update update)
    {
        if (update.Message?.Text == "/menu")
        {
            await client.SendTextMessageAsync(
                chatId: update.Message.Chat,
                text: "Виберіть пункт",
                replyMarkup: telegramBot.replyKeyboard
            );
        }
        else
        {
            switch (update.Message?.Text)
            {
                case "\u26A1 Типи електрокотлів":
                    await client.SendTextMessageAsync(
                   chatId: update.Message.Chat,
                   text: "Ось типи електрокотлів",
                   replyMarkup: telegramBot.replyKeyboard2
                   );
                    break;
                case "\U0001F4D9 Контактна інформація":
                    await client.SendTextMessageAsync(
                   chatId: update.Message.Chat,
                   text: "option 2"
                   );
                    break;
                case "\U00002139\U0000FE0F Інформація про бота":
                    await client.SendTextMessageAsync(
                   chatId: update.Message.Chat,
                   text: "option 3"
                   );
                    break;
                case "\U0001F916 Чат з АІ":
                    await client.SendTextMessageAsync(
                   chatId: update.Message.Chat,
                   text: "option 4"
                   );
                    break;
                default:
                    await client.SendTextMessageAsync(
                   chatId: update.Message.Chat,
                   text: "no option selected"
                   );
                    break;
            }
        }
    }



    public static void Main(string[] args)
    {
        telegramBot.Start();
        telegramBot.OnMessage += OnMessage;
        Console.ReadLine();
    }

    
}