using Telegram.Bot;
using Telegram.Bot.Types;
using Factory.AuthBot.EnvironmentVariables;

namespace Factory.AuthBot;

public class BotService
{
    private readonly TelegramBotClient _telegramBotClient;
    private readonly ILogger _logger;
    
    public BotService(
        TelegramBotClient telegramBotClient,
        Logger<BotService> logger)
    {
        _telegramBotClient = telegramBotClient;
        
        _telegramBotClient.StartReceiving(Update, Error);
        _logger = logger;
    }

    public void StartPulling()
    {
        _telegramBotClient.StartReceiving(Update, Error);
    }
    
    private void Update(
        ITelegramBotClient botClient,
        Update update,
        CancellationToken cancellationToken)
    {
        var message = update.Message?.Text;
        var userId = update.Message?.Chat.Username;
        var chatId = update.Message?.Chat.Id;

        var token = Guid.NewGuid();

        botClient.SendTextMessageAsync(chatId, token.ToString(), cancellationToken: cancellationToken);
    }

    private void Error(
        ITelegramBotClient botClient,
        Exception ex,
        CancellationToken cancellationToken)
    {
        _logger.LogError(
            "{ExceptionType}: There's some error. {ExceptionMessage}",
            ex.GetType(),
            ex.Message);
    }
}