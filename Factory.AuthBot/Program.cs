using BLL;
using BLL.Interfaces;
using Dal.Dapper;
using DAL.Interfaces;
using Dapper;
using Factory.AuthBot;
using Factory.AuthBot.EnvironmentVariables;
using RabbitMQ.Client;
using Telegram.Bot;

var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false);

IConfiguration config = builder.Build();

var conf = config.GetSection("EnvironmentVariables").Get<EnvironmentVariables>();
DefaultTypeMap.MatchNamesWithUnderscores = true;
IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddLogging();

        services.AddSingleton<IUserDao, UserDao>();
        services.AddSingleton<IUserLogic, UserLogic>();
        services.AddSingleton<BotService>();
        services.AddSingleton(s =>
        {
            var bot = new TelegramBotClient(conf?.TelegramBotToken);
            return bot; 
        });

        services.AddSingleton(s =>
        {
            var factory = new ConnectionFactory()
            {
                HostName = conf?.RabbitMqServerHostName,
                UserName = "orlov",
                Password = "2323"
            };
            var connection = factory.CreateConnection();
            return connection;
        });
        
        services.AddStackExchangeRedisCache(options => {
            options.Configuration = "localhost:6379";
            options.InstanceName = "local";
        });
        
        
        services.AddHostedService<Worker>();
    })
    .Build();

host.Run();