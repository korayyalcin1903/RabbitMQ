using MassTransit;
using RabbitMQ.ESB.MassTransit.RequestResponsePattern.Consumer.Consumers;

Console.WriteLine("Consumer");

string rabbitMQUri = "amqps://aedqltkd:znYq84Ah2qLfBa0xUq6Zoe8XO38fg8PF@moose.rmq.cloudamqp.com/aedqltkd";

string requestQueueName = "request-queue";

IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(rabbitMQUri);

    factory.ReceiveEndpoint(requestQueueName, endpoint =>
    {
        endpoint.Consumer<RequestMessageConsumer>();
    });
});

await bus.StartAsync();

Console.Read();