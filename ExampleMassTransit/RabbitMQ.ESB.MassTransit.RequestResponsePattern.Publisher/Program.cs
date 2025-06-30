using MassTransit;
using RabbitMQ.ESB.MassTransit.Shared.RequestResponseMessages;

Console.WriteLine("Publisher");

string rabbitMQUri = "amqps://aedqltkd:znYq84Ah2qLfBa0xUq6Zoe8XO38fg8PF@moose.rmq.cloudamqp.com/aedqltkd";

string requestQueueName = "request-queue";

IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(rabbitMQUri);
});

await bus.StartAsync();

var request = bus.CreateRequestClient<RequestMessage>(new Uri($"{rabbitMQUri}/{requestQueueName}"));

int i = 1;
while (true)
{
    await Task.Delay(200);
    var response = await request.GetResponse<ResponseMessage>(new() { MessageNo = i, Text = $"{i++}.request"});

    Console.WriteLine($"Response Received: {response.Message.Text}");
}

Console.Read();
