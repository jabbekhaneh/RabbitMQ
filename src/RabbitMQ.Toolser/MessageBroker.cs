using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMQ.Toolser;

public class MessageBroker
{
    private readonly ConnectionFactory _connectionFactory;
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly string _queueName = string.Empty;
    public MessageBroker(string hostname = "localhost", string queueName = "first")
    {
        _connectionFactory = new ConnectionFactory
        {
            HostName = hostname,

        };
        _connection = _connectionFactory.CreateConnection();
        _channel = _connection.CreateModel();
        _queueName = queueName;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="queueName"></param>
    /// <param name="message"></param>
    public void Sender(string message = "I`m sender")
    {
        var messageByte = Encoding.UTF8.GetBytes(message).ToArray();
        _channel.QueueDeclare(_queueName, false, false, false, null);
        _channel.BasicPublish("exChange", _queueName, null, messageByte);
        Console.WriteLine("Your message send : " + message);
    }
    /// <summary>
    /// 
    /// </summary>
    public void Resiver()
    {
        var cunsumer = new EventingBasicConsumer(_channel);
        cunsumer.Received += Cunsumer_Received;
        _channel.BasicConsume(_queueName, true, cunsumer);
    }

    private void Cunsumer_Received(object? sender, BasicDeliverEventArgs e)
    {
        var body = e.Body.ToArray();
        var stringMessage = Encoding.UTF8.GetString(body);
        Console.WriteLine($"[-] Message received: {stringMessage}");
    }
}
