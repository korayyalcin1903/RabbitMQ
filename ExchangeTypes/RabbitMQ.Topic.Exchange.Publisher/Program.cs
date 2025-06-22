using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://aedqltkd:znYq84Ah2qLfBa0xUq6Zoe8XO38fg8PF@moose.rmq.cloudamqp.com/aedqltkd");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "topic-exhange-example", type: ExchangeType.Topic);

for (int i = 0; i < 100; i++)
{
    await Task.Delay(200);
    byte[] message = Encoding.UTF8.GetBytes($"Message {i}");
    Console.Write("Topic giriniz: ");
    string topic = Console.ReadLine();
    channel.BasicPublish(
        exchange: "topic-exhange-example",
        routingKey: topic,
        body: message);
}

Console.Read();