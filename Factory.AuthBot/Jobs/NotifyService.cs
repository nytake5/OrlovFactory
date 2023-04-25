using BLL.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Factory.AuthBot.Jobs;

public class NotifyService
{
    private readonly TelegramBotClient _bot;

    public NotifyService(
        TelegramBotClient bot)
    {
        _bot = bot;
    }
    
    public async Task ExecuteAsync(long chatId)
    {
        await _bot.SendTextMessageAsync(
            chatId, 
            "Don't forget to log in after a long absence");
    }
}