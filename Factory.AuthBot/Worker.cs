using BLL;
using BLL.Interfaces;
using DAL.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Telegram.Bot;

namespace Factory.AuthBot;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly BotService _botService;
    public Worker(
        ILogger<Worker> logger,
        BotService botService)
    {
        _logger = logger;
        _botService = botService;
    }
 
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            _botService.StartPulling();
            await Task.Delay(1000, stoppingToken);
    }
}