using RabbitMQ.Toolser;

var rabbitMq = new MessageBroker();
string message = string.Empty;

while (!string.IsNullOrEmpty(message))
{
    Console.Write("Type your message and press [Enter]: ");
    message = Console.ReadLine();
}

rabbitMq.Sender(message:message);






