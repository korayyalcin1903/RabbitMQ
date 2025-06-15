// Bağlantı oluşturma

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new Uri("amqps://aedqltkd:znYq84Ah2qLfBa0xUq6Zoe8XO38fg8PF@moose.rmq.cloudamqp.com/aedqltkd");

// Bağlantıyı aktifleştirme ve kanal açma

using var connection = await factory.CreateConnectionAsync();
using var channel = await connection.CreateChannelAsync();


// Queue oluşturma

await channel.QueueDeclareAsync(queue: "example-queue", exclusive: false); // COnsumerda da kuyruk publisher daki ile aynı yapılandıma tanımlanmlıdır.

// Queue Mesaj Okuma
AsyncEventingBasicConsumer consumer = new(channel);
await channel.BasicConsumeAsync(queue: "example-queue", autoAck: false, consumer:consumer);
consumer.ReceivedAsync += async (sender, e) =>
{
    // Kuyruğa gelen mesajın işlendiği yerdir
    // e.Body: kuyruğa gelen mesajın içeriğini tutar.
    // e.Body.Span veya e.Body.ToArray(): Kuyruktaki mesajın byte dizisine erişebiliriz.
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
};

Console.Read();