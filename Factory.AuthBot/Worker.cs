using BLL.Interfaces;
using Telegram.Bot;

namespace Factory.AuthBot;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly BotService _service;
    public Worker(
        ILogger<Worker> logger,
        IServiceProvider serviceProvider)   
    {
        _logger = logger;
        using var scope = serviceProvider.CreateScope();
        var logic = scope.ServiceProvider.GetRequiredService<IUserLogic>();
        var bot = scope.ServiceProvider.GetRequiredService<TelegramBotClient>();
        _service = new BotService(logic, bot);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

        _service.StartPulling();
    }
}