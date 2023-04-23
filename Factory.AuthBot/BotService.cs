using System.Text;
using BLL.Interfaces;
using RabbitMQ.Client;
using Telegram.Bot;
using Telegram.Bot.Types;
using User = Entities.User;

namespace Factory.AuthBot;

public class BotService
{
    private readonly TelegramBotClient _telegramBotClient;
    private readonly IUserLogic _logic;
    private readonly IModel _channel;
    private string PreviousMessage { get; set; }
    
    public BotService(
        IUserLogic logic, 
        TelegramBotClient telegramBotClient,
        IConnection connection)
    {
        _logic = logic;
        _telegramBotClient = telegramBotClient;
        var factory = new ConnectionFactory { HostName = "localhost" }; 
        _channel = connection.CreateModel();
        _channel.QueueDeclare(queue: "QueueUsers",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
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
                    await LogDefaultMessage(username);
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

    private async Task LogDefaultMessage(
        string message)
    {
        message += " " + "успешно авторизован";
        var body = Encoding.UTF8.GetBytes(message);
        
        _channel.BasicPublish(exchange: string.Empty,
            routingKey: "hello",
            basicProperties: null,  
            body: body);
    }
}