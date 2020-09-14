using System;
using RabbitMQ.Client;
using System.Text;

namespace Send
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

                var message = "Hellow World!";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(
                    exchange: string.Empty,
                    routingKey:"hello",
                    body: body);
                Console.WriteLine($"[X] Sent {message}");
            }

            Console.WriteLine("Press [Any Key] to exit.");
            Console.ReadKey();
        }
    }
}
