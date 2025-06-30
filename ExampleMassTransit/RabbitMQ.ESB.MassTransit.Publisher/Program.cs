using MassTransit;
using RabbitMQ.ESB.MassTransit.Shared.Messages;

string rabbitMQUri = "amqps://aedqltkd:znYq84Ah2qLfBa0xUq6Zoe8XO38fg8PF@moose.rmq.cloudamqp.com/aedqltkd";

string queueName = "example-queue";

IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(rabbitMQUri);
});

ISendEndpoint sendEndpoint = await bus.GetSendEndpoint(new($"{rabbitMQUri}/{queueName}"));

Console.Write("Gönderilecek mesaj: ");
string message = Console.ReadLine();
await sendEndpoint.Send<IMessage>(new ExampleMessage() { Text = message });

Console.Read();