using RabbitMQ.Client;
using System.Text;

// Bağlantı oluşturma
ConnectionFactory factory = new();
factory.Uri = new Uri("amqps://aedqltkd:znYq84Ah2qLfBa0xUq6Zoe8XO38fg8PF@moose.rmq.cloudamqp.com/aedqltkd");

// Bağlantıyı aktifleştirme ve kanal açma
using var connection = await factory.CreateConnectionAsync();
using var channel = await connection.CreateChannelAsync();

// Queue oluşturma
await channel.QueueDeclareAsync(queue: "example-queue", exclusive: false);

// Queue Mesaj gönderme

// RabbitMQ kuyruğa atacağı mesajları byte türünden kabul eder. Mesalarımızı byte dönüştürmemiz gerekecek.
for(int i = 0; i < 100; i++)
{
    await Task.Delay(200);
    byte[] message = Encoding.UTF8.GetBytes("Merhaba" + i);
    await channel.BasicPublishAsync(exchange: "", routingKey: "example-queue", body: message);
}


Console.Read();