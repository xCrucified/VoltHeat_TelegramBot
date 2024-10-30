using Newtonsoft.Json;
using System.Text;
using Telegram.Bot;
using System.Net.Http;
using RestSharp;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using VoltHeat_TelegramBot;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection;

public class Program
{
    public static string token { get; set; } = "7580066425:AAHM1S9ybfK4t_ZxYc-wky0vtLuJEXWQOQU";
    private static readonly string openAiApiKey = "sksk-admin-FGTm8SQqXfXumbp6mg0NsNq6orUkeO-mi12lNH9k-rnCHEBhsNQisXjBEQT3BlbkFJm-zVMPv3TGT-uXh1hwyIgL75H0mf0mnL33WJoftMIrhE-CyR6zMoH3q-IA";
    public static Host telegramBot = new(token);

    private static async Task<string> GetChatGptResponse(string message)
    {
        try
        {
            var client = new RestClient("https://api.openai.com/v1/chat/completions");
            var request = new RestRequest(Method.Post);

            // Корректно установим заголовки
            request.AddHeader("Authorization", $"Bearer {openAiApiKey}");
            request.AddHeader("Content-Type", "application/json");

            // Создание JSON тела запроса
            var requestBody = new
            {
                model = "gpt-3.5-turbo",  // Убедитесь, что модель существует и доступна
                messages = new[]
                {
                new { role = "user", content = message }
            }
            };

            request.AddJsonBody(requestBody); // RestSharp автоматически сериализует объект в JSON
            var response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                dynamic responseObject = JsonConvert.DeserializeObject(response.Content);
                return responseObject.choices[0].message.content.ToString();
            }
            else
            {
                Console.WriteLine($"Ошибка запроса: {response.StatusCode} - {response.Content}");
                return "Произошла ошибка при обращении к ChatGPT.";
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Исключение: {ex.Message}");
            return "Произошла ошибка при обработке запроса.";
        }
    }
    private static async void OnMessage(ITelegramBotClient client, Update update)
    {
        if (update.Message?.Text == "/start")
        {
            await client.SendTextMessageAsync(
                chatId: update.Message.Chat,
                text: "Виберіть пункт",
                replyMarkup: telegramBot.replyKeyboard
            );
        }
        else
        {
            string tmp, text;
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
                   text: "\U0001F4D9Контактна інформація \n\n" +
                   "\U0001F48EДиректор компанії: Конончук Максим (mxxknk@gmail.com)\n" +
                   "\U0001F6E0Головний конструктор: Антонюк Олександр (+380-95-401-7825)\n" +
                   "\U0001F454Заступник директора: Кушнір Ілля (+380-63-549-7932)\n" +
                   "\U0001F527Інженер-конструктор: Моргунов Віталій (+380-97-171-3622)"
                   );
                    break;
                case "\U00002139\U0000FE0F Інформація про бота":
                    await client.SendTextMessageAsync(
                   chatId: update.Message.Chat,
                   text: "ℹ️ Цей бот надає інформацію та виконує різні корисні функції для користувачів. Використовуйте команди для отримання допомоги або доступу до функцій бота."
                   );
                    break;
                case "\U0001F916 Чат з АІ":
                    await client.SendTextMessageAsync(
                        chatId: update.Message.Chat,
                        text: "Запитайте в мене що завгодно"
                    );
                    break;
                case "ТЕНовий котел":
                    tmp = "C:\\Users\\User\\source\\repos\\Projects\\VoltHeat_TelegramBot\\VoltHeat_TelegramBot\\Images\\1st_kotel.jpg";
                    text = "ТЕНовий котел\nОпалення є одним із найпоширеніших як у побутовому, так і в промисловому використанні.\r\nПереваги:\r\n1) Простота і надійність: проста конструкція ТЕНових котлів забезпечує їх надійність і зручність в обслуговуванні. Вони ідеально підходять для домовласників, які віддають перевагу простим і зрозумілим технологіям.\r\n2) Економія: ці котли часто дешевші під час купівлі та встановлення, порівняно з іншими видами електрокотлів.\r\nНедоліки:\r\n1) Утворення накипу: в регіонах з жорсткою водою ТЕНи можуть швидко заростати накипом, що вимагає їх регулярного чищення або заміни.\r\n2) Чутливість до напруги: у більшості випадків знадобляться запобіжник від перепадів напруги в мережі.";
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
                    tmp = "C:\\Users\\User\\source\\repos\\Projects\\VoltHeat_TelegramBot\\VoltHeat_TelegramBot\\Images\\2nd_kotel.jpg";
                    text = "Індукційний котел\nОпалення вважається одними з найсучасніших і найефективніших видів електричних котлів.\r\nПереваги:\r\n1) Висока ефективність: завдяки використанню індукційного нагріву знижується енергоспоживання та експлуатаційні витрати, що робить ці котли найбільш ефективнішими серед всіх типів електрокотлів.\r\n2) Більш низькі витрати: завдяки більш ефективній роботі індукційний котел витрачає менше електроенергії (до 30%). На тривалій дистанції відбиває вищу ціну порівняно з ТЕНовими моделями.\r\n3) Довговічність: відсутність рухомих частин і менший ризик утворення накипу збільшують термін служби котла (до 40 років).\r\nНедоліки:\r\n1) Висока вартість: індукційні котли зазвичай дорожчі за ТЕНові (в середньому в 2-5 разів).\r\n2) Габарити і вага: побутові моделі можуть важити до 80 кг.\r\n3) Складність в установці та обслуговуванні: для установки і обслуговування може знадобитися спеціалізоване обладнання та вміння.";
                    using (var stream = new FileStream(tmp, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        await client.SendPhotoAsync(
                            chatId: update.Message.Chat,
                            photo: new InputFileStream(stream, Path.GetFileName(tmp)),
                            caption: text
                        );
                    }
                    break;
                case "Електродний котел":
                    tmp = "C:\\Users\\User\\source\\repos\\Projects\\VoltHeat_TelegramBot\\VoltHeat_TelegramBot\\Images\\3rd_kotel.jpg";
                    text = "Електродний котел\nЦі котли відрізняється від традиційного методу нагрівання за допомогою нагрівальних елементів, таких як ТЕНи.\r\nПереваги:\r\n1) Високий ККД: завдяки прямому нагріванню теплоносія, електродні котли мають високий ККД (в районі 100%).\r\n2) Компактні розміри: вони займають менше місця порівняно з іншими типами котлів.\r\n3) Простота встановлення та обслуговування: електродні котли мають простішу конструкцію, що спрощує і монтаж, і догляд за обладнанням.\r\nНедоліки:\r\n1) Вимоги до якості води: для роботи електродних котлів потрібна вода з певним рівнем мінералів, щоб вона могла проводити електрику. Вода не повинна бути занадто чистою або занадто жорсткою, щоб уникнути швидкого зношення електродів і утворення накипу.\r\n2) Регулярний моніторинг і контроль якості води є невід’ємною частиною експлуатації електродного котла.\r\n3) Зношування електродів: з часом електроди будуть зношуватись, що вимагатиме їх заміни.";
                    using (var stream = new FileStream(tmp, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        await client.SendPhotoAsync(
                            chatId: update.Message.Chat,
                            photo: new InputFileStream(stream, Path.GetFileName(tmp)),
                            caption: text
                        );
                    }
                    break;
                case "\u25C0\uFE0F Назад":
                    await client.SendTextMessageAsync(
                    chatId: update.Message.Chat,
                    text: "Виберіть пункт",
                    replyMarkup: telegramBot.replyKeyboard
                    );
                    break;
                default:
                    string response = await GetChatGptResponse(update.Message.Text);
                    await client.SendTextMessageAsync(
                        chatId: update.Message.Chat,
                        text:response
                    );
                    Console.WriteLine($"ChatGpt: { response }");
                    break;
            }
        }
    }



    public static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        telegramBot.Start();
        telegramBot.OnMessage += OnMessage;
        Console.ReadLine();
    }


}