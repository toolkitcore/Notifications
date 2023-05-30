using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Notifications.Application.Common.Interfaces;
using Notifications.Domain.Configurations;
using Notifications.Infrastructure.Common.Extensions;
using RabbitMQ.Client;

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
        
        //Create the RabbitMQ connection using connection factory details as i mentioned above
        using var connection = factory.CreateConnection();
        //Here we create channel with session and model
        using var channel = connection.CreateModel();
        //declare the queue after mentioning name and a few property related to that
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