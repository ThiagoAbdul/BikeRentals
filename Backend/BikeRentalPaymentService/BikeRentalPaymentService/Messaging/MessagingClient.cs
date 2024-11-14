using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Channels;

namespace BikeRentalPaymentService.Message;

public class MessagingClient(IConfiguration configuration)
{


    public IModel GetChannel(string queue)
    {
        string userName = Environment.GetEnvironmentVariable("MESSAGE_USERNAME")
            ?? configuration.GetValue<string>("Message:UserName")!;
        string password = Environment.GetEnvironmentVariable("MESSAGE_PASSWORD")
            ?? configuration.GetValue<string>("Message:Password")!;

        ConnectionFactory factory = new()
        {
            HostName = "localhost",
            UserName = userName,
            Password = password
        };
        IConnection connection = factory.CreateConnection();

        IModel channel = connection.CreateModel();

        channel.QueueDeclare(queue,
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

        return channel;
    }

    public void SendObject(object payload, string queue)
    {
        string message = JsonSerializer.Serialize(payload);
        Send(message, queue);
    }

    public void Send(string message, string queue) 
    {
        using IModel channel = GetChannel(queue);


        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(exchange: string.Empty,
                             routingKey: queue,
                             basicProperties: null,
                             body: body);
    }
}
