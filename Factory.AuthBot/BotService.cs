using BLL.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using User = Entities.User;

namespace Factory.AuthBot;

public class BotService
{
    private readonly TelegramBotClient _telegramBotClient;
    private readonly IUserLogic _logic;
    
    private string PreviousMessage { get; set; }
    
    public BotService(
        IUserLogic logic, 
        TelegramBotClient telegramBotClient)
    {
        _logic = logic;
        _telegramBotClient = telegramBotClient;
    }

    public void StartPulling()
    {
        PreviousMessage = "/start"; 
        _telegramBotClient.StartReceiving(Update, Error);
    }
    
    private async Task Update(
        ITelegramBotClient botClient,
        Update update,  
        CancellationToken cancellationToken)
     { 
        var message = update.Message?.Text;
        var username = update.Message?.Chat.Username;
        var chatId = update.Message?.Chat?.Id;        
        
        switch (message)
        {   
            case "/start":
                var userByChatId = await _logic.GetUserByChatId(chatId.Value);
                if (userByChatId == null)
                {
                    await botClient.SendTextMessageAsync(
                        chatId, "Entry password", cancellationToken: cancellationToken);
                }
                else
                {
                    var token = Guid.NewGuid();
                    await botClient.SendTextMessageAsync(chatId, token.ToString(), cancellationToken: cancellationToken);
                    await _logic.TokenizeUser(username, token);
                }
                break;
            default:
                if (await _logic.LoginUser(new User() { Login = username, Password = message}))
                {
                    var token = Guid.NewGuid();
                    await botClient.SendTextMessageAsync(chatId, token.ToString(),  cancellationToken: cancellationToken);
                    await _logic.TokenizeUser(username, token);
                }
                else
                { 
                    await botClient.SendTextMessageAsync(
                        chatId, "Password wrong!", cancellationToken: cancellationToken);
                }
                break;
        }

        await Task.CompletedTask;
     }
    
    private async Task Error(
        ITelegramBotClient botClient,
        Exception ex,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }
}