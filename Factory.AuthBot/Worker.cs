namespace Factory.AuthBot;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly BotService _service;

    public Worker(
        ILogger<Worker> logger,
        BotService service)
    {
        _logger = logger;
        _service = service;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

        _service.StartPulling();
    }
}