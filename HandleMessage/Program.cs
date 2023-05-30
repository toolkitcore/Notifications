using System.Text;
using Newtonsoft.Json;
using Notifications.Domain.Entities;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var factory = new ConnectionFactory()
{
    HostName = "localhost", // Địa chỉ IP hoặc tên miền của máy chủ RabbitMQ
    Port = 5672, // Cổng mặc định của RabbitMQ
    UserName = "guest", // Tên người dùng
    Password = "guest", // Mật khẩu
    ClientProvidedName = "Rabbit Receiver1 App"
};

IConnection connection = factory.CreateConnection();

IModel channel = connection.CreateModel();

var name = typeof(NotificationGroup).Name;
string exchangeName = name;
string routingKey = name;
string queueName = name;

channel.QueueDeclare(queueName, false, false, false, null);
channel.QueueBind(queueName, exchangeName, routingKey, null);
channel.BasicQos(0, 1, false);

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (sender, args) =>
{
    var body = args.Body.ToArray();
    string message = Encoding.UTF8.GetString(body);
    var notificationCreated = JsonConvert.DeserializeObject<NotificationGroup>(message);

    Console.WriteLine($"Message Received: {notificationCreated.Id} - {notificationCreated.Name} create successful.");
    
    channel.BasicAck(args.DeliveryTag, false);
};

string consumerTag = channel.BasicConsume(queueName, false, consumer);

Console.ReadLine();

channel.BasicCancel(consumerTag);

channel.Close();
connection.Close();

