using Factory.AuthBot;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddLogging();
    })
    .Build();



host.Run();