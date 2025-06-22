using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://aedqltkd:znYq84Ah2qLfBa0xUq6Zoe8XO38fg8PF@moose.rmq.cloudamqp.com/aedqltkd");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "topic-exhange-example", type: ExchangeType.Topic);

Console.Write("Dinlenecek topic giriniz: ");
string topic = Console.ReadLine();
string queueName = channel.QueueDeclare().QueueName;
channel.QueueBind(queue: queueName, exchange: "topic-exhange-example", routingKey: topic);

EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
consumer.Received += (model, ea) =>
{
    string message = Encoding.UTF8.GetString(ea.Body.Span);
    Console.WriteLine(message);
};

Console.Read();