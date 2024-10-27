using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using VoltHeat_TelegramBot;

public class Program
{
    public static string token { get; set; } = "7580066425:AAHM1S9ybfK4t_ZxYc-wky0vtLuJEXWQOQU";
    public static void Main(string[] args)
    {
        Host telegramBot = new(token);
        telegramBot.Start();
        Console.ReadLine();
    }
}