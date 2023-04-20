using Telegram.Bot;
using Telegram.Bot.Types;
using Factory.AuthBot.EnvironmentVariables;

namespace Factory.AuthBot;

public class BotService
{
    private readonly TelegramBotClient _telegramBotClient;
    
    public BotService(TelegramBotClient telegramBotClient)
    {
        _telegramBotClient = telegramBotClient;
        
        _telegramBotClient.StartReceiving(Update, Error);
    }
    
    private void Update(
        ITelegramBotClient botClient,
        Update updateLogic,
        CancellationToken cancellationToken)
    {
        var messageText  = updateLogic.Message?.Text;
        var chatId = updateLogic.Message?.Chat.Id;

        var token = Guid.NewGuid();

        botClient.SendTextMessageAsync(chatId, token.ToString(), cancellationToken: cancellationToken);
    }

    private void Error(
        ITelegramBotClient botClient,
        Exception updateLogic,
        CancellationToken cancellationToken)
    {
        
    }
}