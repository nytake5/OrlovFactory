using System.Diagnostics;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Factory.WebApi.Service;

public class UserMessageHandler : BackgroundService
{
    private IConnection _connection;
    private IModel _channel;
    
    public UserMessageHandler(
        IConnection connection,
        IModel channel)
    {
        _connection = connection;
        _channel = channel;
        _channel.QueueDeclare(queue: "UsersQueue", durable: false, exclusive: false, autoDelete: false,
            arguments: null);
    }
    
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (ch, ea) =>
        {
            var content = Encoding.UTF8.GetString(ea.Body.ToArray());
			
            Debug.WriteLine($"Получено сообщение: {content}");

            _channel.BasicAck(ea.DeliveryTag, false);
        };

        _channel.BasicConsume("UsersQueue", false, consumer);

        return Task.CompletedTask;
    }
    
    public override void Dispose()
    {
        _channel.Close();
        _connection.Close();
        base.Dispose();
    }
}