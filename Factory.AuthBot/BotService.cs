using BLL.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Factory.AuthBot.EnvironmentVariables;

namespace Factory.AuthBot;

public class BotService
{
    private readonly TelegramBotClient _telegramBotClient;
    private readonly IUserLogic _logic;
    
    public BotService(
        IUserLogic logic, 
        TelegramBotClient telegramBotClient)
    {
        _logic = logic;
        _telegramBotClient = telegramBotClient;
        
        _telegramBotClient.StartReceiving(Update, Error);
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
        
    }
}