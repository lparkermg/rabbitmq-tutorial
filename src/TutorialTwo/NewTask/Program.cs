using System;
using RabbitMQ.Client;
using System.Text;

namespace NewTask
{
    internal sealed class Program
    {
        private static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using(var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(
                    queue: "hello",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var message = (args.Length > 0) ? string.Join(" ", args) : "Hello World!";
                var body = Encoding.UTF8.GetBytes(message);

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                channel.BasicPublish(
                    exchange: string.Empty,
                    routingKey: "task_queue",
                    basicProperties: properties,
                    body: body);
                Console.WriteLine($"[X] Sent {message}");
            }

            Console.WriteLine("Press [Any Key] to exit.");
            Console.ReadKey();
        }
    }
}
