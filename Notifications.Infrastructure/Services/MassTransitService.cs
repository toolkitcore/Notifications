using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Notifications.Application.Common.Interfaces;
using Notifications.Domain.Configurations;
using RabbitMQ.Client;
using Shared.Utilities;

namespace Notifications.Infrastructure.Services;

public class MassTransitService : IMassTransitService
{
    private readonly MassTransitSetting _massTransitSetting;
    public MassTransitService(IConfiguration configuration)
    {
        _massTransitSetting = configuration.GetOptions<MassTransitSetting>() ?? throw new ArgumentNullException(nameof(configuration));
    }
    public void SendMessage<T>(T message)
    {
        var factory = new ConnectionFactory {
            HostName = _massTransitSetting.HostName,
            Port = _massTransitSetting.Port,
            UserName = _massTransitSetting.UserName,
            Password = _massTransitSetting.Password,
            ClientProvidedName = _massTransitSetting.ClientProvidedName
        };
        
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        var name = typeof(T).Name;
        channel.QueueDeclare(name, durable: false, exclusive: false, autoDelete: false, arguments: null);
        var settings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };
        var json = JsonConvert.SerializeObject(message, settings);
        var body = Encoding.UTF8.GetBytes(json);
        
        // Publish the message with the properties
        channel.BasicPublish(exchange: "", routingKey: name, body: body);
    }
}