using BLL;
using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using Factory.AuthBot;
using Factory.AuthBot.EnvironmentVariables;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using Telegram.Bot;

var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false);

IConfiguration config = builder.Build();

var conf = config.GetSection("EnvironmentVariables").Get<EnvironmentVariables>();

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddLogging();

        services.AddScoped<IUserDao, UserDao>();
        services.AddScoped<IUserLogic, UserLogic>();
        services.AddSingleton(s =>
        {
            var bot = new TelegramBotClient(conf.TelegramBotToken);
            return bot;
        });
        
        services.AddDbContextFactory<FactoryContext>(
            optionsAction =>
            {
                optionsAction
                    .UseNpgsql(conf?.NpgsqlConnectionString);
            });

        services.AddSingleton(s =>
        {
            var factory = new ConnectionFactory()
            {
                HostName = conf?.RabbitMqServerHostName
            };
            var connection = factory.CreateConnection();
            return connection;
        });
    })
    .Build();

host.Run();