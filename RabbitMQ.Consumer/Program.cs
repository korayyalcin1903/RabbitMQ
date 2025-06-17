// Bağlantı oluşturma

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new Uri("amqps://aedqltkd:znYq84Ah2qLfBa0xUq6Zoe8XO38fg8PF@moose.rmq.cloudamqp.com/aedqltkd");

// Bağlantıyı aktifleştirme ve kanal açma

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();


// Queue oluşturma

channel.QueueDeclare(queue: "example-queue", exclusive: false, durable: true); // COnsumerda da kuyruk publisher daki ile aynı yapılandıma tanımlanmlıdır.

// Queue Mesaj Okuma
EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: "example-queue", autoAck: false, consumer:consumer);
channel.BasicQos(0, 1, false);
consumer.Received += async (sender, e) =>
{
    // Kuyruğa gelen mesajın işlendiği yerdir
    // e.Body: kuyruğa gelen mesajın içeriğini tutar.
    // e.Body.Span veya e.Body.ToArray(): Kuyruktaki mesajın byte dizisine erişebiliriz.
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
    // channel.BasicAck(deliveryTag: e.DeliveryTag, multiple: false);
    // channel.BasicAck(deliveryTag: e.DeliveryTag, multiple: false);

    await Task.CompletedTask;
};

Console.Read();